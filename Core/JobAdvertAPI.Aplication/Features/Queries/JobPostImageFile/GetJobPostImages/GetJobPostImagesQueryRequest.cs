using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Aplication.Features.Queries.JobPostImageFile.GetJobPostImages
{
    public class GetJobPostImagesQueryRequest : IRequest<List<GetJobPostImagesQueryResponse>>
    {
        public int Id { get; set; }
    }
}
