using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Aplication.Features.Commands.JobPostImageFile.RemoveJobPostImage
{
    public class RemoveJobPostImageCommandRequest : IRequest<RemoveJobPostImageCommandResponse>
    {
        public int Id { get; set; }
        public int? ImageId { get; set; }
    }
}
