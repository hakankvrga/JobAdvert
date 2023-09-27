using JobAdvertAPI.Aplication.Repositories;
using JobAdvertAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace JobAdvertAPI.Aplication.Features.Queries.JobPost.GetByIdJobPost
{
    public class GetByIdJobPostQueryHandler : IRequestHandler<GetByIdJobPostQueryRequest, GetByIdJobPostQueryResponse>
    {
        readonly  IJobPostReadRepository _jobPostReadRepository;

        public GetByIdJobPostQueryHandler(IJobPostReadRepository jobPostReadRepository)
        {
            _jobPostReadRepository = jobPostReadRepository;
        }

        public async Task<GetByIdJobPostQueryResponse> Handle(GetByIdJobPostQueryRequest request, CancellationToken cancellationToken)
        {
          JobAdvertAPI.Domain.Entities.JobPost jobPost=  await _jobPostReadRepository.GetByIdAsync(request.Id);
            return new()
            {
                
                UserId = jobPost.UserId,
                CompanyName = jobPost.CompanyName,
                Description = jobPost.Description,
                EndDate = jobPost.EndDate,
                StartDate = jobPost.StartDate,
                Title = jobPost.Title
            };
        }
    }
}
