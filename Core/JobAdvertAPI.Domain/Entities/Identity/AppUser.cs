using Microsoft.AspNetCore.Identity;

namespace JobAdvertAPI.Domain.Entities.Identity;

public  class AppUser : IdentityUser<string>
{
    public string NameSurname { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenEndDate { get; set; }

    
    public ICollection<JobPostAppUser> JobPostAppUsers { get; set; }
    public virtual ICollection<UserJobPost> UserJobPosts { get; set; } = new List<UserJobPost>();
}
