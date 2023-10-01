using JobAdvertAPI.Aplication.Repositories;
using JobAdvertAPI.Domain.Entities;
using JobAdvertAPI.Persistence.Contexts;

namespace JobAdvertAPI.Persistence.Repositories;

internal class JobPostAppUserWriteRepository : WriteRepository<JobPostAppUser>, IJobPostAppUserWriteRepository
{
    public JobPostAppUserWriteRepository(JobAdvertContext context) : base(context)
    {
    }
}
