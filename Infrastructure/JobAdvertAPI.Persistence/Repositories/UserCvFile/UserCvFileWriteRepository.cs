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
    public class UserCvFileWriteRepository : WriteRepository<UserCvFile>, IUserCvFileWriteRepository
    {
        public UserCvFileWriteRepository(JobAdvertContext context) : base(context)
        {
        }
    }
}
