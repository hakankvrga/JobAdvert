using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Aplication.Features.Queries.JobPostImageFile.GetJobPostImages
{
    public class GetJobPostImagesQueryResponse
    {
        public string Path { get; set; }
        public string FileName { get; set; }
        public int Id { get; set; }
    }
}
