using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Aplication.Features.Commands.JobPost.UpdateJobPost
{
    public class UpdateJobPostCommandRequest : IRequest<UpdateJobPostCommandResponse>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
      
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
