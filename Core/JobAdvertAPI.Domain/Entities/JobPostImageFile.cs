using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Domain.Entities
{
    public class JobPostImageFile : File
    {
        public ICollection<JobPost> JobPosts { get; set; }
    }
}
