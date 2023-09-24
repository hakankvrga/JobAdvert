using Azure.Core;
using JobAdvertAPI.Aplication.Abstractions.Services;
using JobAdvertAPI.Aplication.DTOs.User;
using JobAdvertAPI.Aplication.Exceptions;
using JobAdvertAPI.Aplication.Features.Commands.AppUser.CreateUser;
using JobAdvertAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUser model)
        {

            IdentityResult result = await _userManager.CreateAsync(new Domain.Entities.Identity.AppUser
            {
                Id = Guid.NewGuid().ToString(),
                NameSurname = model.NameSurname,
                UserName = model.UserName,
                Email = model.Email
            }, model.Password);


            CreateUserResponse response = new() { Succeeded = result.Succeeded };
            if (result.Succeeded)
                response.Message = "Kullanıcı başarıyla oluşturuldu";
            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}\n";
            return response;
        }

        public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accesTokenDate, int addOnAccessTokenDate)
        {
            
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accesTokenDate.AddSeconds( addOnAccessTokenDate);
                await _userManager.UpdateAsync(user);
            }
            else
                throw new NotFoundUserException();
            
        }
    }
}

