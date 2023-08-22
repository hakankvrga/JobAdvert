using JobAdvertAPI.Domain.Entities.common;
using System;
using System.Collections.Generic;

namespace JobAdvertAPI.Domain.Entities;

public partial class User : BaseEntity
{
    
    public int UserTypeId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? ContactNumber { get; set; }

    public string? Cv { get; set; }

   

    public virtual ICollection<UserJobPost> UserJobPosts { get; set; } = new List<UserJobPost>();

    public virtual UserType UserType { get; set; } = null!;
}
