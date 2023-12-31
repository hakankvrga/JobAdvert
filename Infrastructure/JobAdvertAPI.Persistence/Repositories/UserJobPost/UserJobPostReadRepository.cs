﻿using JobAdvertAPI.Aplication.Repositories;
using JobAdvertAPI.Domain.Entities;
using JobAdvertAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Persistence.Repositories
{
    public class UserJobPostReadRepository : ReadRepository<UserJobPost>, IUserJobPostReadRepository
    {
        public UserJobPostReadRepository(JobAdvertContext context) : base(context)
        {
        }
    }
}
