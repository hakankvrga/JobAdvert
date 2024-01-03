using JobAdvertAPI.Aplication.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JobAdvertAPI.Aplication.Features.Queries.JobPost.GetAllJobPost;

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
       
        var totalJobPostCount = _jobPostReadRepository.GetAll(false).Count(); // toplam iş ilanı sayısı alındı
        var jobPosts = _jobPostReadRepository.GetAll().Skip(request.Page * request.Size).Take(request.Size) // sayfalama işlemi yapıldı
            .Include(j => j.JobPostImageFiles)                
            .Select(j => new// iş ilanı bilgileri alındı
        {
            j.Id,           
            j.CompanyName,
            j.Description,
            j.StartDate,
            j.EndDate,
            j.Title,
            j.JobPostImageFiles

        }).ToList(); // listeye dönüştürüldü
        return new()
        {
            JobPosts = jobPosts,
            TotalJobPostCount = totalJobPostCount
        }; // iş ilanları ve toplam iş ilanı sayısı döndürüldü
    }
}
