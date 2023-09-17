using JobAdvertAPI.Aplication.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Aplication.Features.Commands.JobPost.UpdateJobPost
{
    public class UpdateJobPostCommandHandler : IRequestHandler<UpdateJobPostCommandRequest, UpdateJobPostCommandResponse>
    {

        readonly IJobPostWriteRepository _jobPostWriteRepository;
        readonly IJobPostReadRepository _jobPostReadRepository;

        public UpdateJobPostCommandHandler(IJobPostWriteRepository jobPostWriteRepository, IJobPostReadRepository jobPostReadRepository)
        {
            _jobPostWriteRepository = jobPostWriteRepository;
            _jobPostReadRepository = jobPostReadRepository;
        }

        public async Task<UpdateJobPostCommandResponse> Handle(UpdateJobPostCommandRequest request, CancellationToken cancellationToken)
        {
            JobAdvertAPI.Domain.Entities.JobPost jobPost = await _jobPostReadRepository.GetByIdAsync(request.Id);
            jobPost.UserId = request.UserId;
            jobPost.JobTypeId = request.JobTypeId;
            jobPost.Title = request.Title;
            jobPost.CompanyName = request.CompanyName;
            jobPost.Description = request.Description;

            jobPost.StartDate = request.StartDate;
            jobPost.EndDate = request.EndDate;
            await _jobPostWriteRepository.SaveAsync();
            return new();
        }
    }
}
