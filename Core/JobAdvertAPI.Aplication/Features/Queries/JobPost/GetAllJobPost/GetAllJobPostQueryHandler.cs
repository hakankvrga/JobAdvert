using JobAdvertAPI.Aplication.Repositories;
using JobAdvertAPI.Aplication.RequestParameters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Aplication.Features.Queries.JobPost.GetAllJobPost
{
    public class GetAllJobPostQueryHandler : IRequestHandler<GetAllJobPostQueryRequest, GetAllJobPostQueryResponse>
    {
        readonly IJobPostReadRepository _jobPostReadRepository;
        readonly ILogger<GetAllJobPostQueryHandler> _logger;
        public GetAllJobPostQueryHandler(IJobPostReadRepository jobPostReadRepository, ILogger<GetAllJobPostQueryHandler> logger)
        {
            _jobPostReadRepository = jobPostReadRepository;
            _logger = logger;
        }
        public async Task<GetAllJobPostQueryResponse> Handle(GetAllJobPostQueryRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get all jobposts");
           
            var totalJobPostCount = _jobPostReadRepository.GetAll(false).Count();
            var jobPosts = _jobPostReadRepository.GetAll().Skip(request.Page * request.Size).Take(request.Size)
                .Include(j => j.JobPostImageFiles)                
                .Select(j => new
            {
                j.Id,
               
                j.CompanyName,
                j.Description,
                j.StartDate,
                j.EndDate,
                j.Title,
                j.JobPostImageFiles

            }).ToList();

            return new()
            {
                JobPosts = jobPosts,
                TotalJobPostCount = totalJobPostCount
            };
        }
    }
}
