using JobAdvertAPI.Aplication.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAdvertAPI.Aplication.Features.Commands.JobPost.RemoveJobPost
{
    public class RemoveJobPostCommandHandler : IRequestHandler<RemoveJobPostCommandRequest, RemoveJobPostCommandResponse>
    {
        readonly IJobPostWriteRepository _jobPostWriteRepository;

        public RemoveJobPostCommandHandler(IJobPostWriteRepository jobPostWriteRepository)
        {
            _jobPostWriteRepository = jobPostWriteRepository;
        }

        public async Task<RemoveJobPostCommandResponse> Handle(RemoveJobPostCommandRequest request, CancellationToken cancellationToken)
        {
            await _jobPostWriteRepository.RemoveAsync(request.Id);
            await _jobPostWriteRepository.SaveAsync();
            return new();
        }
    }
}
