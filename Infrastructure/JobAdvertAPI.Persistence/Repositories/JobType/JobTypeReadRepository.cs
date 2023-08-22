using JobAdvertAPI.Aplication.Repositories;
using JobAdvertAPI.Domain.Entities;
using JobAdvertAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Persistence.Repositories
{
    public class JobTypeReadRepository : ReadRepository<JobType>, IJobTypeReadRepository
    {
        public JobTypeReadRepository(JobAdvertContext context) : base(context)
        {
        }
    }
}
