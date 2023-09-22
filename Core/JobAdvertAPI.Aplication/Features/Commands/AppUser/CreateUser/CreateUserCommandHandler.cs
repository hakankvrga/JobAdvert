using JobAdvertAPI.Aplication.Abstractions.Services;
using JobAdvertAPI.Aplication.DTOs.User;
using JobAdvertAPI.Aplication.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Aplication.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {

            CreateUserResponse response = await _userService.CreateAsync(new()
            {
                Email = request.email,
                NameSurname = request.nameSurname,
                UserName = request.userName,
                Password = request.password,
                PasswordConfirm = request.passwordConfirm
            });

            return new()
            {
                Message= response.Message,
                Succeeded = response.Succeeded
            };





            //throw new UserCreateFailedException();

        }
    }
}
