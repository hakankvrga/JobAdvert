using JobAdvertAPI.Aplication.Features.Queries.JobPost.ApplyJobPost;
using JobAdvertAPI.Aplication.Features.Queries.JobPostImageFile.GetJobPostImages;
using JobAdvertAPI.Aplication.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class ApplyJobPostQueryHandler : IRequestHandler<ApplyJobPostQueryRequest, ApplyJobPostQueryResponse>
{
    readonly IJobPostReadRepository _jobPostReadRepository;
    readonly IMediator _mediator;

    public ApplyJobPostQueryHandler(IJobPostReadRepository jobPostReadRepository, IMediator mediator)
    {
        _jobPostReadRepository = jobPostReadRepository;
        _mediator = mediator;
    }

    public async Task<ApplyJobPostQueryResponse> Handle(ApplyJobPostQueryRequest request, CancellationToken cancellationToken)
    {
        JobAdvertAPI.Domain.Entities.JobPost jobPost = await _jobPostReadRepository.GetByIdAsync(request.Id);

        var getImagesRequest = new GetJobPostImagesQueryRequest { Id = jobPost.Id };
        List<GetJobPostImagesQueryResponse> images = await _mediator.Send(getImagesRequest);

        
        var imagePaths = images.Select(image => image.Path).ToList();

        var response = new ApplyJobPostQueryResponse
        {
            JobPostId = jobPost.Id,
            Title = jobPost.Title,
            CompanyName = jobPost.CompanyName,
            Description = jobPost.Description,
            StartDate = jobPost.StartDate,
            EndDate = jobPost.EndDate,
            Images = imagePaths,
        };

        return response;
    }
}
