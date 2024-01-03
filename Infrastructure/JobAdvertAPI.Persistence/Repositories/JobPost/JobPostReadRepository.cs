using JobAdvertAPI.Aplication.Repositories;
using JobAdvertAPI.Domain.Entities;
using JobAdvertAPI.Persistence.Contexts;

namespace JobAdvertAPI.Persistence.Repositories;

public class JobPostReadRepository : ReadRepository<JobPost>, IJobPostReadRepository
{
    public JobPostReadRepository(JobAdvertContext context) : base(context)
    {
    }
}
