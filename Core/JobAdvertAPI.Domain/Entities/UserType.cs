using JobAdvertAPI.Domain.Entities.common;
using System;
using System.Collections.Generic;

namespace JobAdvertAPI.Domain.Entities;

public partial class UserType : BaseEntity
{
    

    public string UserType1 { get; set; } = null!;

    

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
