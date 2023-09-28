using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Domain.Entities.Identity
{
    public class AppRole : IdentityRole<string>
    {
        public const string EmployerRole = "Employer";
        public const string NormalUserRole = "NormalUser";
    }
}
