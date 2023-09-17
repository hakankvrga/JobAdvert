using Azure.Core;
using JobAdvertAPI.Aplication.Abstractions.Storage;
using JobAdvertAPI.Aplication.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Aplication.Features.Commands.JobPostImageFile.UploadJobPostImage
{
    public class UploadJobPostImageCommandHandler : IRequestHandler<UploadJobPostImageCommandRequest, UploadJobPostImageCommandResponse>
    {
        readonly IJobPostImageFileWriteRepository _jobPostImageFileWriteRepository;
        readonly IJobPostReadRepository _jobPostReadRepository;
        readonly IStorageService _storageService;

        public UploadJobPostImageCommandHandler(IJobPostImageFileWriteRepository jobPostImageFileWriteRepository, IJobPostReadRepository jobPostReadRepository, IStorageService storageService)
        {
            _jobPostImageFileWriteRepository = jobPostImageFileWriteRepository;
            _jobPostReadRepository = jobPostReadRepository;
            _storageService = storageService;
        }

        public async Task<UploadJobPostImageCommandResponse> Handle(UploadJobPostImageCommandRequest request, CancellationToken cancellationToken)
        {
            List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("photo-images", request.Files);


           Domain.Entities.JobPost jobPost = await _jobPostReadRepository.GetByIdAsync(request.Id);

            await _jobPostImageFileWriteRepository.AddRangeAsync(result.Select(r => new Domain.Entities.JobPostImageFile
            {
                FileName = r.fileName,
                Path = r.pathOrContainerName,
                Storage = _storageService.StorageName,
                JobPosts = new List<Domain.Entities.JobPost>() { jobPost }
            }).ToList());

            await _jobPostImageFileWriteRepository.SaveAsync();
            return new();
        }
    }
}
