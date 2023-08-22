using JobAdvertAPI.Domain.Entities.common;
using System;
using System.Collections.Generic;

namespace JobAdvertAPI.Domain.Entities;

public partial class JobType : BaseEntity
{
    

    public string Type { get; set; } = null!;

    
    public virtual ICollection<JobPost> JobPosts { get; set; } = new List<JobPost>();
}
