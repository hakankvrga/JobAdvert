using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Aplication.Features.Queries.JobPost.GetAllJobPost
{
    public class GetAllJobPostQueryResponse
    {
        public int TotalJobPostCount { get; set; }
        public object JobPosts { get; set; }
    }
}
