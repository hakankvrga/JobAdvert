using JobAdvertAPI.Aplication.Abstractions.Services;
using JobAdvertAPI.Aplication.DTOs.User;
using JobAdvertAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Aplication.Features.Commands.AppUser.CreateUser.CreateNormalUser
{
    public class CreateNormalUserCommandHandler : IRequestHandler<CreateNormalUserCommandRequest, CreateNormalUserCommandResponse>
    {
        readonly IUserService _userService;
        private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;


        public CreateNormalUserCommandHandler(IUserService userService, UserManager<Domain.Entities.Identity.AppUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        public async Task<CreateNormalUserCommandResponse> Handle(CreateNormalUserCommandRequest request, CancellationToken cancellationToken)
        {

            CreateUserResponse response = await _userService.CreateAsync(new()
            {
                Email = request.email,
                NameSurname = request.nameSurname,
                UserName = request.userName,
                Password = request.password,
                PasswordConfirm = request.passwordConfirm
            });

            if (response.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(request.userName);
                if (user != null)
                {
                    await _userManager.AddToRoleAsync(user, AppRole.NormalUserRole);
                }
            }
            return new()
            {
                Message = response.Message,
                Succeeded = response.Succeeded
            };
        }
    }
}