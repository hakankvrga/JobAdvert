using JobAdvertAPI.Domain.Entities.Identity;

namespace JobAdvertAPI.Domain.Entities;

public class JobPostAppUser 
{
    public int JobPostId { get; set; }

    public string AppUserId { get; set; }
   
    public JobPost JobPost { get; set; }
    public AppUser AppUser { get; set; }

}
