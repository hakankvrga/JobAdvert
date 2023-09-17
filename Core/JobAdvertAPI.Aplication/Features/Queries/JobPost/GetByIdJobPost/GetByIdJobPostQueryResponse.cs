using JobAdvertAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Aplication.Features.Queries.JobPost.GetByIdJobPost
{
    public class GetByIdJobPostQueryResponse
    {
        public int UserId { get; set; }

        public int JobTypeId { get; set; }

        public string Title { get; set; } = null!;

        public string CompanyName { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }





        //public virtual JobType JobType { get; set; } = null!;
        //public ICollection<JobPostImageFile> JobPostImageFiles { get; set; }

        //public virtual ICollection<UserJobPost> UserJobPosts { get; set; } = new List<UserJobPost>();
    }
}
