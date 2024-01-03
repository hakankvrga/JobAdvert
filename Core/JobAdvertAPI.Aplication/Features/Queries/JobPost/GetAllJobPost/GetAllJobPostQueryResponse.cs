namespace JobAdvertAPI.Aplication.Features.Queries.JobPost.GetAllJobPost;

public class GetAllJobPostQueryResponse
{
    public int TotalJobPostCount { get; set; }
    public object JobPosts { get; set; }
}
