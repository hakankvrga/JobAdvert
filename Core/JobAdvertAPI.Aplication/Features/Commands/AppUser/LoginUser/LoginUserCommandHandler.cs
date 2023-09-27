﻿using JobAdvertAPI.Aplication.Abstractions.Services;
using JobAdvertAPI.Aplication.Abstractions.Token;
using JobAdvertAPI.Aplication.DTOs;
using JobAdvertAPI.Aplication.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Aplication.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
       readonly IAuthService _authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
           var token= await _authService.LoginAsync(request.UsernameOrEmail, request.Password, 900);

            return new LoginUserSuccessCommandResponse()
            {
                Token = token
            };
        }
    }
    
    
}
