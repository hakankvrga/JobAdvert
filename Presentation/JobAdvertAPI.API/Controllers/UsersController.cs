﻿using JobAdvertAPI.Aplication.Features.Commands.AppUser.CreateUser;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace JobAdvertAPI.API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
       
        readonly  IMediator _mediator;


        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest )
        {
          CreateUserCommandResponse response=  await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }

        

    }
}
