using JobAdvertAPI.Aplication.DTOs.User;
using JobAdvertAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Aplication.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUser model);
        Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accesTokenDate, int addOnAccessTokenDate);
    }
}
