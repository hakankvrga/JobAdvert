using JobAdvertAPI.Aplication.Repositories;
using JobAdvertAPI.Aplication.RequestParameters;
using MediatR;
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
        public GetAllJobPostQueryHandler(IJobPostReadRepository jobPostReadRepository)
        {
            _jobPostReadRepository = jobPostReadRepository;
        }
        public async Task<GetAllJobPostQueryResponse> Handle(GetAllJobPostQueryRequest request, CancellationToken cancellationToken)
        {
            var totalCount = _jobPostReadRepository.GetAll(false).Count();
            var jobPosts = _jobPostReadRepository.GetAll().Skip(request.Page * request.Size).Take(request.Size).Select(j => new
            {
                j.Id,
                j.JobTypeId,
                j.CompanyName,
                j.Description,
                j.StartDate,
                j.EndDate,
                j.Title,

            }).ToList();

            return new()
            {
                JobPosts = jobPosts,
                TotalCount = totalCount
            };
        }
    }
}
