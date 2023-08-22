using JobAdvertAPI.Domain.Entities.common;
using System;
using System.Collections.Generic;

namespace JobAdvertAPI.Domain.Entities;

public partial class ApplyStatus : BaseEntity
{
   

    public string ApplyStatus1 { get; set; } = null!;

    

    public virtual ICollection<UserJobPost> UserJobPosts { get; set; } = new List<UserJobPost>();
}
