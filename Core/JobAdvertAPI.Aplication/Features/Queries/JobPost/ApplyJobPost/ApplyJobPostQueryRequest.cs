using MediatR;

namespace JobAdvertAPI.Aplication.Features.Queries.JobPost.ApplyJobPost;

public class ApplyJobPostQueryRequest : IRequest<ApplyJobPostQueryResponse>
{
    public int Id { get; set; }
}
