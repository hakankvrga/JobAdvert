using JobAdvertAPI.Aplication.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Aplication.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandResponse
    {
        
        
    }

    public class LoginUserSuccessCommandResponse : LoginUserCommandResponse
    {
        
        
            public Token Token { get; set; }
    
    }

    public class LoginUserFailCommandResponse : LoginUserCommandResponse
    {
        
       
            public string Message { get; set; }
       

        
    }
}
