using JobAdvertAPI.Aplication.Repositories;
using JobAdvertAPI.Domain.Entities;
using JobAdvertAPI.Persistence.Contexts;

namespace JobAdvertAPI.Persistence.Repositories;

internal class JobPostAppUserReadRepository : ReadRepository<JobPostAppUser>, IJobPostAppUserReadRepository
{
    public JobPostAppUserReadRepository(JobAdvertContext context) : base(context)
    {
    }
}
