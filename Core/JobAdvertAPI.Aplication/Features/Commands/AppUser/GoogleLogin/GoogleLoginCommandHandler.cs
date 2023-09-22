
using JobAdvertAPI.Aplication.Abstractions.Services;
using JobAdvertAPI.Aplication.Abstractions.Token;
using JobAdvertAPI.Aplication.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Aplication.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {

        readonly IAuthService _authService;

        public GoogleLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {

        var token=   await  _authService.GoogleLoginAsync(request.IdToken, 15);

            return new()
            {
                Token = token
            };


        }
    }


}
