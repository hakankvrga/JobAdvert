using JobAdvertAPI.Aplication.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Aplication.Features.Commands.JobPost.CreateJobPost
{
    public class CreateJobPostCommandHandler : IRequestHandler<CreateJobPostCommandRequest, CreateJobPostCommandResponse>
    {
        readonly IJobPostWriteRepository _jobPostWriteRepository;

        public CreateJobPostCommandHandler(IJobPostWriteRepository jobPostWriteRepository)
        {
            _jobPostWriteRepository = jobPostWriteRepository;
        }

        public async Task<CreateJobPostCommandResponse> Handle(CreateJobPostCommandRequest request, CancellationToken cancellationToken)
        {
            await _jobPostWriteRepository.AddAsync(new()
            {
                UserId = request.UserId,
                JobTypeId = request.JobTypeId,
                Title = request.Title,
                CompanyName = request.CompanyName,
                Description = request.Description,

                StartDate = request.StartDate,
                EndDate = request.EndDate,
            });
            await _jobPostWriteRepository.SaveAsync();
            return new();
        }
    }
}
