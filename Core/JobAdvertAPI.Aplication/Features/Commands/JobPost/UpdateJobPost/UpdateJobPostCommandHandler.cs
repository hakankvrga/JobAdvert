using JobAdvertAPI.Aplication.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
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
        readonly ILogger<UpdateJobPostCommandHandler> _logger;

        public UpdateJobPostCommandHandler(IJobPostWriteRepository jobPostWriteRepository, IJobPostReadRepository jobPostReadRepository, ILogger<UpdateJobPostCommandHandler> logger)
        {
            _jobPostWriteRepository = jobPostWriteRepository;
            _jobPostReadRepository = jobPostReadRepository;
            _logger = logger;
        }

        public async Task<UpdateJobPostCommandResponse> Handle(UpdateJobPostCommandRequest request, CancellationToken cancellationToken)
        {
            JobAdvertAPI.Domain.Entities.JobPost jobPost = await _jobPostReadRepository.GetByIdAsync(request.Id);
            
            
            jobPost.Title = request.Title;
            jobPost.CompanyName = request.CompanyName;
            jobPost.Description = request.Description;

            jobPost.StartDate = request.StartDate;
            jobPost.EndDate = request.EndDate;
            await _jobPostWriteRepository.SaveAsync();
            _logger.LogInformation("İlan güncellendi.");
            return new();
        }
    }
}
