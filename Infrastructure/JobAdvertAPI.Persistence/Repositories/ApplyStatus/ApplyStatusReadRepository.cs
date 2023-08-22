using JobAdvertAPI.Domain.Entities;
using JobAdvertAPI.Aplication.Repositories;
using JobAdvertAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Persistence.Repositories
{
    public class ApplyStatusReadRepository : ReadRepository<ApplyStatus>, IApplyStatusReadRepository
    {
        public ApplyStatusReadRepository(JobAdvertContext context) : base(context)
        {
        }
    }
}
