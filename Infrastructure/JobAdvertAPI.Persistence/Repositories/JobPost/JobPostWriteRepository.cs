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
    public class JobPostWriteRepository : WriteRepository<JobPost>, IJobPostWriteRepository
    {
        public JobPostWriteRepository(JobAdvertContext context) : base(context)
        {
        }
    }
}
