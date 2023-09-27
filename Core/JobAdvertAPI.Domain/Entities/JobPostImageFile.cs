using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Domain.Entities
{
    public class JobPostImageFile : File
    {
        public bool Showcase { get; set; }
        public ICollection<JobPost> JobPosts { get; set; }
    }
}
