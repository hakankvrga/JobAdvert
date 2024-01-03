using JobAdvertAPI.Aplication.Repositories;
using MediatR;

namespace JobAdvertAPI.Aplication.Features.Commands.JobPost.CreateJobPost;

public class CreateJobPostCommandHandler : IRequestHandler<CreateJobPostCommandRequest, CreateJobPostCommandResponse> //iş ilanı oluşturulabilmesi için handler sınıfı yazıldı

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
            Title = request.Title,
            CompanyName = request.CompanyName,
            Description = request.Description,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
        });
        await _jobPostWriteRepository.SaveAsync();// iş ilanı bilgileri alınarak kayıt işlemi oluşturuldu.
        return new();
    }
}
