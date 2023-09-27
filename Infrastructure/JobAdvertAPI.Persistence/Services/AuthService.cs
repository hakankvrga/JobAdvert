using Azure.Core;
using Google.Apis.Auth;
using JobAdvertAPI.Aplication.Abstractions.Services;
using JobAdvertAPI.Aplication.Abstractions.Token;
using JobAdvertAPI.Aplication.DTOs;
using JobAdvertAPI.Aplication.Exceptions;
using JobAdvertAPI.Aplication.Features.Commands.AppUser.LoginUser;
using JobAdvertAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Persistence.Services
{
    public class AuthService : IAuthService
    {
        readonly IConfiguration _configuration;
        readonly UserManager<AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;
        readonly SignInManager<AppUser> _signInManager;
        readonly IUserService _userService;

        public AuthService(IConfiguration configuration, UserManager<Domain.Entities.Identity.AppUser> userManager, ITokenHandler tokenHandler, SignInManager<AppUser> signInManager, IUserService userService)
        {
            _configuration = configuration;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _signInManager = signInManager;
            _userService = userService;
        }

        async  Task<Token> CreateUserExternalAsync(AppUser user,string email,string givenName,string name, UserLoginInfo info, int accessTokenLifeTime)
        {
            bool result = user != null;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = email,
                        UserName = givenName,
                        NameSurname = name,


                    };
                    var identityResult = await _userManager.CreateAsync(user);


                    if (identityResult.Succeeded)
                    {
                        // Kullanıcı başarıyla oluşturuldu.
                        result = true;
                    }
                    else
                    {
                        // Kullanıcı oluşturma işlemi başarısız oldu.
                        result = false;

                        // Hataları kontrol etmek ve işlemi nedenin başarısız olduğunu anlamak için
                        // identityResult.Errors koleksiyonunu kullanabilirsiniz.
                        foreach (var error in identityResult.Errors)
                        {
                            // Hatanın ayrıntılarını loglayabilir veya hata mesajlarını inceleyebilirsiniz.
                            Console.WriteLine($"Hata: {error.Code}, Mesaj: {error.Description}");
                        }
                    }
                }
            }

            if (result)
                await _userManager.AddLoginAsync(user, info);
            else
                throw new Exception("Invalid external authentication");

            Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
            await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 900);


            return token;
        }

        public async Task<Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration["ExternalLoginSettings:Google:Client_ID"] }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

            var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");

            Domain.Entities.Identity.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            return await CreateUserExternalAsync(user, payload.Email, payload.GivenName, payload.Name, info, accessTokenLifeTime);
        }

        public async Task<Token> LoginAsync(string usernameOrEmail, string password ,int accessTokenLifeTime)
        {
            Domain.Entities.Identity.AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(usernameOrEmail);
                if (user == null)
                {
                    throw new NotFoundUserException();
                }
            }


            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (result.Succeeded) // auth başarılı
            {
                Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 900);
                return token;
            }
            //return new LoginUserFailCommandResponse()
            //{
            //    Message = "Kullanıcı adı veya şifre hatalı"
            //};
            throw new AuthenticationErrorException();

        }

        public  async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user= await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
            if (user == null && user?.RefreshTokenEndDate > DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateAccessToken(15, user); 
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 1500);
                return token;
            }
            else
                throw new NotFoundUserException();
        }
    }
}
