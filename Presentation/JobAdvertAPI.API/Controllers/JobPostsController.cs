using JobAdvertAPI.Aplication.Abstractions.Storage;
using JobAdvertAPI.Aplication.Features.Commands.JobPost.CreateJobPost;
using JobAdvertAPI.Aplication.Features.Commands.JobPost.RemoveJobPost;
using JobAdvertAPI.Aplication.Features.Commands.JobPost.UpdateJobPost;
using JobAdvertAPI.Aplication.Features.Commands.JobPostImageFile.RemoveJobPostImage;
using JobAdvertAPI.Aplication.Features.Commands.JobPostImageFile.UploadJobPostImage;
using JobAdvertAPI.Aplication.Features.Queries.JobPost.GetAllJobPost;
using JobAdvertAPI.Aplication.Features.Queries.JobPost.GetByIdJobPost;
using JobAdvertAPI.Aplication.Features.Queries.JobPostImageFile.GetJobPostImages;
using JobAdvertAPI.Aplication.Repositories;
using JobAdvertAPI.Aplication.RequestParameters;

using JobAdvertAPI.Aplication.ViewModels.JobPosts;
using JobAdvertAPI.Aplication.ViewModels.Users;
using JobAdvertAPI.Domain.Entities;
using JobAdvertAPI.Persistence.Repositories;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace JobAdvertAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPostsController : ControllerBase
    {
      
       

        readonly IMediator _mediator;

        
        public JobPostsController( IMediator mediator)
        {
            
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllJobPostQueryRequest getAllJobPostQueryRequest)
        {
          GetAllJobPostQueryResponse response= await  _mediator.Send(getAllJobPostQueryRequest);
           return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetByIdJobPostQueryRequest getByIdJobPostQueryRequest)
        {
           GetByIdJobPostQueryResponse response= await _mediator.Send(getByIdJobPostQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateJobPostCommandRequest createJobPostCommandRequest)
        {
          CreateJobPostCommandResponse response=  await _mediator.Send(createJobPostCommandRequest);
            
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UpdateJobPostCommandRequest updateJobPostCommandRequest)
        {

         UpdateJobPostCommandResponse response= await _mediator.Send(updateJobPostCommandRequest);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute]RemoveJobPostCommandRequest removeJobPostCommandRequest )
        {
         RemoveJobPostCommandResponse response =  await _mediator.Send(removeJobPostCommandRequest);    
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery] UploadJobPostImageCommandRequest uploadJobPostImageCommandRequest)
        {
           uploadJobPostImageCommandRequest.Files = Request.Form.Files;
            UploadJobPostImageCommandResponse uploadJobPostImageCommandResponse= await _mediator.Send(uploadJobPostImageCommandRequest);
            return Ok();
        }


        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetJobPostImages([FromRoute]GetJobPostImagesQueryRequest postImagesQueryRequest)
        {
            List<GetJobPostImagesQueryResponse> response= await _mediator.Send(postImagesQueryRequest);
            return Ok(response);
        }


        [HttpDelete("[action]/{Id}")]
        public async Task<IActionResult> DeleteJobPostImage([ FromRoute]RemoveJobPostImageCommandRequest removeJobPostImageCommandRequest , [FromQuery] int imageId)
        {
            removeJobPostImageCommandRequest.ImageId = imageId;
            RemoveJobPostImageCommandResponse response= await _mediator.Send(removeJobPostImageCommandRequest);
            return Ok();
        }
    }
}
