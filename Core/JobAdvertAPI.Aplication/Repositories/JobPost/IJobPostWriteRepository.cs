﻿using JobAdvertAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Aplication.Repositories
{
    public interface IJobPostWriteRepository : IWriteRepository<JobPost>
    {
    }
}
