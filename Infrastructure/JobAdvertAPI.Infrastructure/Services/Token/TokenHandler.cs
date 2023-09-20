using JobAdvertAPI.Aplication.Abstractions.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Aplication.DTOs.Token CreateAccessToken(int minute)
        {
           Aplication.DTOs.Token token = new ();

            // security key in simetriğini alıyoruz
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            //Şifrelenmiş kimliğimizi oluşturuyoruz
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            //oluşturulacak token ayarlarını veriyoruz
            token.Expiration = DateTime.UtcNow.AddMinutes(minute);
            JwtSecurityToken securityToken= new(
                               issuer: _configuration["Token:Issuer"],
                               audience: _configuration["Token:Audience"],
                               expires: token.Expiration,
                               notBefore: DateTime.UtcNow,
                               signingCredentials: signingCredentials
                                               );

            //token oluşturucu sınıfından bir örnek alıyoruz
            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken=  tokenHandler.WriteToken(securityToken);


            return token;
        }
    }
}
