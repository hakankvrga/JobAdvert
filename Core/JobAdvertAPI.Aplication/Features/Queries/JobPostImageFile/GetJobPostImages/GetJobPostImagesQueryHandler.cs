using JobAdvertAPI.Aplication.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Aplication.Features.Queries.JobPostImageFile.GetJobPostImages
{
    public class GetJobPostImagesQueryHandler : IRequestHandler<GetJobPostImagesQueryRequest, List<GetJobPostImagesQueryResponse>>
    {
        readonly IJobPostReadRepository _jobPostReadRepository;
        readonly IConfiguration configuration;

        public GetJobPostImagesQueryHandler(IJobPostReadRepository jobPostReadRepository, IConfiguration configuration)
        {
            _jobPostReadRepository = jobPostReadRepository;
            this.configuration = configuration;
        }

        public async Task<List<GetJobPostImagesQueryResponse>> Handle(GetJobPostImagesQueryRequest request, CancellationToken cancellationToken)
        {
           Domain.Entities.JobPost? jobPost = await _jobPostReadRepository.Table.Include(j => j.JobPostImageFiles).FirstOrDefaultAsync(j => j.Id == request.Id);
            return jobPost?.JobPostImageFiles.Select(j => new GetJobPostImagesQueryResponse
            {
                Path = $"{configuration["BaseStoragaUrl"]}/{j.Path}",
               FileName= j.FileName,
               Id = j.Id

            }).ToList();
        }
    }
}
