using JobAdvertAPI.Aplication.Repositories;
using JobAdvertAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Persistence.Repositories
{
    public class FileWriteRepository : WriteRepository<JobAdvertAPI.Domain.Entities.File>, IFileWriteRepository
    {
        public FileWriteRepository(JobAdvertContext context) : base(context)
        {
        }
    }
}
