using JobAdvertAPI.Aplication.Abstractions.Services.Authentications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Aplication.Abstractions.Services
{
    public interface IAuthService :  IInternalAuthentication, IExternalAuthentication
    {
    }
}
