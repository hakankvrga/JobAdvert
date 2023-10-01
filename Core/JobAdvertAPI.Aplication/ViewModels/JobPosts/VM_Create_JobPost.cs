using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Aplication.ViewModels.JobPosts
{
    public class VM_Create_JobPost
    {
        
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
