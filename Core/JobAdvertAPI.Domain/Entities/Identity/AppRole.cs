using Microsoft.AspNetCore.Identity;

namespace JobAdvertAPI.Domain.Entities.Identity
{
    public class AppRole : IdentityRole<string>
    {
        public const string EmployerRole = "Employer";
        public const string NormalUserRole = "NormalUser";
    }
}
