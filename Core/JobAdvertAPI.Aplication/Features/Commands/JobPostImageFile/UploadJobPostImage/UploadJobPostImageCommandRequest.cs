using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Aplication.Features.Commands.JobPostImageFile.UploadJobPostImage
{
    public class UploadJobPostImageCommandRequest : IRequest<UploadJobPostImageCommandResponse>
    {
        public int Id { get; set; }
        public IFormFileCollection? Files { get; set; }
    }
}
