using JobAdvertAPI.Aplication.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace JobAdvertAPI.Aplication.Features.Commands.JobPostImageFile.ChangeShowcaseImage
{
    public class ChangeShowcaseImageCommandHandler : IRequestHandler<ChangeShowcaseImageCommandRequest, ChangeShowcaseImageCommandResponse>
    {
      readonly IJobPostImageFileWriteRepository _jobPostImageFileWriteRepository;

        public ChangeShowcaseImageCommandHandler(IJobPostImageFileWriteRepository jobPostImageFileWriteRepository)
        {
            _jobPostImageFileWriteRepository = jobPostImageFileWriteRepository;
        }

        public async Task<ChangeShowcaseImageCommandResponse> Handle(ChangeShowcaseImageCommandRequest request, CancellationToken cancellationToken)
        {
            
                var query = _jobPostImageFileWriteRepository.Table
                      .Include(p => p.JobPosts)
                      .SelectMany(p => p.JobPosts, (pif, p) => new
                      {
                          pif,
                          p
                      });

            var data = await query.FirstOrDefaultAsync(p => p.p.Id == int.Parse(request.JobPostId) && p.pif.Showcase);

            if (data != null)
                data.pif.Showcase = false;

            var image = await query.FirstOrDefaultAsync(p => p.pif.Id == int.Parse(request.ImageId));
            if (image != null)
                image.pif.Showcase = true;

            await _jobPostImageFileWriteRepository.SaveAsync();

            return new();

        }
    }
}
