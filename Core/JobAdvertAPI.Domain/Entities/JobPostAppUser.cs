using JobAdvertAPI.Domain.Entities.common;
using JobAdvertAPI.Domain.Entities.Identity;

namespace JobAdvertAPI.Domain.Entities;

public class JobPostAppUser : BaseEntity
{
    public int JobPostId { get; set; }
    public JobPost JobPost { get; set; }

    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }

}
