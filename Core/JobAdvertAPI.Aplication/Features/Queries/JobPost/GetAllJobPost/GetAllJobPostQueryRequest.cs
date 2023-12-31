﻿using MediatR;

namespace JobAdvertAPI.Aplication.Features.Queries.JobPost.GetAllJobPost;

public class GetAllJobPostQueryRequest : IRequest<GetAllJobPostQueryResponse>
{
    //public Pagination Pagination { get; set; }
    public int Page { get; set; } = 0;
    public int Size { get; set; } = 5;
}
