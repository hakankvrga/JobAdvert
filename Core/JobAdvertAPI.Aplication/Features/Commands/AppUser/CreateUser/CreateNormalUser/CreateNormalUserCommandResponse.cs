using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Aplication.Features.Commands.AppUser.CreateUser.CreateNormalUser
{
    public class CreateNormalUserCommandResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public Guid UserId { get; set; }
    }
}
