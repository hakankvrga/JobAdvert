
using JobAdvertAPI.Aplication.Features.Commands.JobPost.CreateJobPost;
using JobAdvertAPI.Aplication.Features.Commands.JobPost.RemoveJobPost;
using JobAdvertAPI.Aplication.Features.Commands.JobPost.UpdateJobPost;
using JobAdvertAPI.Aplication.Features.Commands.JobPostImageFile.ChangeShowcaseImage;
using JobAdvertAPI.Aplication.Features.Commands.JobPostImageFile.RemoveJobPostImage;
using JobAdvertAPI.Aplication.Features.Commands.JobPostImageFile.UploadJobPostImage;
using JobAdvertAPI.Aplication.Features.Queries.JobPost.ApplyJobPost;
using JobAdvertAPI.Aplication.Features.Queries.JobPost.GetAllJobPost;
using JobAdvertAPI.Aplication.Features.Queries.JobPost.GetByIdJobPost;
using JobAdvertAPI.Aplication.Features.Queries.JobPostImageFile.GetJobPostImages;



using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Net;

namespace JobAdvertAPI.API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class JobPostsController : ControllerBase
{
    readonly IMediator _mediator;  
    public JobPostsController( IMediator mediator)
    {        
        _mediator = mediator;
    }

    [HttpGet("apply/{Id}")]
    public async Task<IActionResult> GetDetailsJobPost([FromRoute] ApplyJobPostQueryRequest applyJobPostQueryRequest)
    {
        ApplyJobPostQueryResponse response = await _mediator.Send(applyJobPostQueryRequest);
        return Ok(response);
    }

    [HttpGet("[action]/{id}")]

    public async Task<IActionResult> GetJobPostImages([FromRoute] GetJobPostImagesQueryRequest postImagesQueryRequest)
    {
        List<GetJobPostImagesQueryResponse> response = await _mediator.Send(postImagesQueryRequest);
        return Ok(response);
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
  
    [Authorize(AuthenticationSchemes = "Employer")]
    public async Task<IActionResult> Post(CreateJobPostCommandRequest createJobPostCommandRequest)
    {
      CreateJobPostCommandResponse response=  await _mediator.Send(createJobPostCommandRequest);
        
        return StatusCode((int)HttpStatusCode.Created);
    }
    [HttpPut]
  
    [Authorize(AuthenticationSchemes = "Employer")]
    public async Task<IActionResult> Put([FromBody]UpdateJobPostCommandRequest updateJobPostCommandRequest)
    {

     UpdateJobPostCommandResponse response= await _mediator.Send(updateJobPostCommandRequest);
        return Ok();
    }

    [HttpDelete("{Id}")]
    [Authorize(AuthenticationSchemes = "Employer")]
    public async Task<IActionResult> Delete([FromRoute]RemoveJobPostCommandRequest removeJobPostCommandRequest )
    {
     RemoveJobPostCommandResponse response =  await _mediator.Send(removeJobPostCommandRequest);    
        return Ok();
    }

    [HttpPost("[action]")]
    [Authorize(AuthenticationSchemes = "Employer")]
    public async Task<IActionResult> Upload([FromQuery] UploadJobPostImageCommandRequest uploadJobPostImageCommandRequest)
    {
       uploadJobPostImageCommandRequest.Files = Request.Form.Files;
        UploadJobPostImageCommandResponse uploadJobPostImageCommandResponse= await _mediator.Send(uploadJobPostImageCommandRequest);
        return Ok();
    }

    [HttpDelete("[action]/{Id}")]
    [Authorize(AuthenticationSchemes = "Employer")]
    public async Task<IActionResult> DeleteJobPostImage([ FromRoute]RemoveJobPostImageCommandRequest removeJobPostImageCommandRequest , [FromQuery] int imageId)
    {
        removeJobPostImageCommandRequest.ImageId = imageId;
        RemoveJobPostImageCommandResponse response= await _mediator.Send(removeJobPostImageCommandRequest);
        return Ok();
    }



    [HttpGet("[action]")]
    [Authorize(AuthenticationSchemes = "Employer")]
    public async Task<IActionResult> ChangeShowcaseImage([FromQuery] ChangeShowcaseImageCommandRequest changeShowcaseImageCommandRequest)
    {
        ChangeShowcaseImageCommandResponse response = await _mediator.Send(changeShowcaseImageCommandRequest);
        return Ok(response);
    }


}
