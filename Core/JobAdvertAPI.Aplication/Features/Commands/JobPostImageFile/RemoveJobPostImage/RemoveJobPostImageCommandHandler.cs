using JobAdvertAPI.Aplication.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace JobAdvertAPI.Aplication.Features.Commands.JobPostImageFile.RemoveJobPostImage
{
    public class RemoveJobPostImageCommandHandler : IRequestHandler<RemoveJobPostImageCommandRequest, RemoveJobPostImageCommandResponse>
    {
        readonly IJobPostWriteRepository _jobPostWriteRepository;
        readonly IJobPostReadRepository _jobPostReadRepository;

        public RemoveJobPostImageCommandHandler(IJobPostWriteRepository jobPostWriteRepository, IJobPostReadRepository jobPostReadRepository)
        {
            _jobPostWriteRepository = jobPostWriteRepository;
            _jobPostReadRepository = jobPostReadRepository;
        }

        public async Task<RemoveJobPostImageCommandResponse> Handle(RemoveJobPostImageCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.JobPost? jobPost = await _jobPostReadRepository.Table.Include(j => j.JobPostImageFiles).FirstOrDefaultAsync(j => j.Id == request.Id);

            Domain.Entities.JobPostImageFile? jobPostImageFile = jobPost?.JobPostImageFiles.FirstOrDefault(j => j.Id == request.ImageId);
           
            if(jobPostImageFile != null) 
            jobPost?.JobPostImageFiles.Remove(jobPostImageFile);
            await _jobPostWriteRepository.SaveAsync();
            return new();
        }
    }
}
