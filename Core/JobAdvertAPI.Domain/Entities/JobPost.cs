using JobAdvertAPI.Domain.Entities.common;
using System;
using System.Collections.Generic;

namespace JobAdvertAPI.Domain.Entities;

public partial class JobPost : BaseEntity
{
    

    public int UserId { get; set; }

    

    public string Title { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    

   

    
    public ICollection<JobPostImageFile> JobPostImageFiles { get; set; }

    public virtual ICollection<UserJobPost> UserJobPosts { get; set; } = new List<UserJobPost>();
}
