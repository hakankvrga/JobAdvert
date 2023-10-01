using Microsoft.AspNetCore.Identity;

namespace JobAdvertAPI.Domain.Entities.Identity;

public sealed class AppUser : IdentityUser<string>
{
    public string NameSurname { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenEndDate { get; set; }


    public ICollection<JobPost> JobPosts { get; set; }
}
