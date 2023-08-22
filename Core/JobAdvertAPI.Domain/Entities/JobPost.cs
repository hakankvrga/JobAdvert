using JobAdvertAPI.Domain.Entities.common;
using System;
using System.Collections.Generic;

namespace JobAdvertAPI.Domain.Entities;

public partial class JobPost : BaseEntity
{
    

    public int UserId { get; set; }

    public int JobTypeId { get; set; }

    public string Title { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string ImagePath { get; set; } = null!;

   

    public virtual JobType JobType { get; set; } = null!;

    public virtual ICollection<UserJobPost> UserJobPosts { get; set; } = new List<UserJobPost>();
}
