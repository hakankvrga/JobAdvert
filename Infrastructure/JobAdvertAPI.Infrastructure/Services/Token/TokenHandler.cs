using JobAdvertAPI.Aplication.Abstractions.Token;
using JobAdvertAPI.Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JobAdvertAPI.Infrastructure.Services.Token;

public class TokenHandler : ITokenHandler
{
    readonly IConfiguration _configuration;

    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Aplication.DTOs.Token CreateAccessToken(int second, AppUser user)
    {
       Aplication.DTOs.Token token = new ();
        // security key in simetriğini alıyoruz
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
        //Şifrelenmiş kimliğimizi oluşturuyoruz
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        //oluşturulacak token ayarlarını veriyoruz
        token.Expiration = DateTime.UtcNow.AddSeconds(second);
        JwtSecurityToken securityToken= new(
                           issuer: _configuration["Token:Issuer"],
                           audience: _configuration["Token:Audience"],
                           expires: token.Expiration,
                           notBefore: DateTime.UtcNow,
                           signingCredentials: signingCredentials,
                           claims: new List<Claim> { new(ClaimTypes.Name, user.UserName) }
                                           );
        //token oluşturucu sınıfından bir örnek alıyoruz
        JwtSecurityTokenHandler tokenHandler = new();
        token.AccessToken=  tokenHandler.WriteToken(securityToken);
        token.RefreshToken = CreateRefreshToken();
        return token;
    }
    public string CreateRefreshToken()
    {
        byte[] number= new byte[32];
        using RandomNumberGenerator random= RandomNumberGenerator.Create();
        random.GetBytes(number);
        return Convert.ToBase64String(number);
        // rastgele sayı üretiyoruz ve bunu stringe cevirerek güvenli bir refresh token oluşturuyoruz
    }
}
