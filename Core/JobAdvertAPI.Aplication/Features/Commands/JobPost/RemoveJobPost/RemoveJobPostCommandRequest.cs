using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Aplication.Features.Commands.JobPost.RemoveJobPost
{
    public class RemoveJobPostCommandRequest : IRequest<RemoveJobPostCommandResponse>
    {
        public int Id { get; set; }
    }
}
