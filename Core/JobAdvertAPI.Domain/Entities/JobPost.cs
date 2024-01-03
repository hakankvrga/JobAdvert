using JobAdvertAPI.Domain.Entities.common;

namespace JobAdvertAPI.Domain.Entities;

public partial class JobPost : BaseEntity
{

    public string Title { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime StartDate { get; set; }


    public ICollection<JobPostAppUser> JobPostAppUsers { get; set; }
    public DateTime EndDate { get; set; }

    public ICollection<JobPostImageFile> JobPostImageFiles { get; set; }
    public virtual ICollection<UserJobPost> UserJobPosts { get; set; } = new List<UserJobPost>();
}
