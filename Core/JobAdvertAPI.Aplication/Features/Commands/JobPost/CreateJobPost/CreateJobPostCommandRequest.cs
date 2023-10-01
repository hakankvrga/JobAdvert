using MediatR;

namespace JobAdvertAPI.Aplication.Features.Commands.JobPost.CreateJobPost;

public class CreateJobPostCommandRequest : IRequest<CreateJobPostCommandResponse>
{

    
    public string Title { get; set; }
    public string CompanyName { get; set; }
    public string Description { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
