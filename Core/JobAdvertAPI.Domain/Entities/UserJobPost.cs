using JobAdvertAPI.Domain.Entities.common;
using System;
using System.Collections.Generic;

namespace JobAdvertAPI.Domain.Entities;

public partial class UserJobPost : BaseEntity
{
        

    public int JobPostId { get; set; }

    public int UserId { get; set; }

    public int ApplyStatusId { get; set; }

    public DateTime ApplyDate { get; set; }

    

    public virtual ApplyStatus ApplyStatus { get; set; } = null!;

    public virtual JobPost JobPost { get; set; } = null!;

    //public virtual User User { get; set; } = null!;
}
