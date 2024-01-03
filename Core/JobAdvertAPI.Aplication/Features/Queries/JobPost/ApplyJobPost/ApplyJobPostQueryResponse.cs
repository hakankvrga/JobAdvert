using JobAdvertAPI.Aplication.Features.Queries.JobPostImageFile.GetJobPostImages;

namespace JobAdvertAPI.Aplication.Features.Queries.JobPost.ApplyJobPost;

public class ApplyJobPostQueryResponse
{
    public int JobPostId { get; set; }
    public string Title { get; set; }
    public string CompanyName { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }


    public List<string> Images { get; set; }
}
