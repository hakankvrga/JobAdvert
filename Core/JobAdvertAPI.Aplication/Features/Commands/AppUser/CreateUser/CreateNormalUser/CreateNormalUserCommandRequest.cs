using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Aplication.Features.Commands.AppUser.CreateUser.CreateNormalUser
{
    public class CreateNormalUserCommandRequest : IRequest<CreateNormalUserCommandResponse>
    {
        public string nameSurname { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string passwordConfirm { get; set; }
    }
    
}
