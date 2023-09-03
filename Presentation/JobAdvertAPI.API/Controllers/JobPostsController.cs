using JobAdvertAPI.Aplication.Repositories;
using JobAdvertAPI.Aplication.RequestParameters;
using JobAdvertAPI.Aplication.ViewModels.JobPosts;
using JobAdvertAPI.Aplication.ViewModels.Users;
using JobAdvertAPI.Domain.Entities;
using JobAdvertAPI.Persistence.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace JobAdvertAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPostsController : Controller
    {
        readonly private IJobPostWriteRepository _jobPostWriteRepository;
        readonly private IJobPostReadRepository _jobPostReadRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public JobPostsController(IJobPostWriteRepository JobPostWriteRepository, IJobPostReadRepository JobPostReadRepository, IWebHostEnvironment webHostEnvironment)
        {
            _jobPostWriteRepository = JobPostWriteRepository;
            _jobPostReadRepository = JobPostReadRepository;
            this._webHostEnvironment = webHostEnvironment;
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]Pagination pagination)
        {

            var totalCount= _jobPostReadRepository.GetAll(false).Count();

            var jobPosts = _jobPostReadRepository.GetAll().Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(j => new
            {
                j.Id,
                j.JobTypeId,
                j.CompanyName,
                j.Description,
                j.StartDate,
                j.EndDate,
                j.Title,
                j.ImagePath
            }).ToList();

            return Ok(new
            {
                jobPosts,
                totalCount
            });
        }

        [HttpGet("id")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _jobPostReadRepository.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_JobPost model)
        {
            await _jobPostWriteRepository.AddAsync(new()
            {
                UserId=model.UserId,
                JobTypeId=model.JobTypeId,
                Title=model.Title,
                CompanyName = model.CompanyName,
                Description=model.Description,
                ImagePath = model.ImagePath,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
            });
            await _jobPostWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_JobPost model)
        {

            JobPost jobPost = await _jobPostReadRepository.GetByIdAsync(model.Id);
            jobPost.UserId = model.UserId;
            jobPost.JobTypeId = model.JobTypeId;
            jobPost.Title = model.Title;
            jobPost.CompanyName = model.CompanyName;
            jobPost.Description = model.Description;
            jobPost.ImagePath = model.ImagePath;
            jobPost.StartDate = model.StartDate;
            jobPost.EndDate = model.EndDate;
            await _jobPostWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            await _jobPostWriteRepository.RemoveAsync(id);
            await _jobPostWriteRepository.SaveAsync();
            return Ok();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
            //wwwroot/resource/jobpost-images
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "resource/jobpost-images");

            if (!Directory.Exists(uploadPath))  
                Directory.CreateDirectory(uploadPath);

            Random r = new();
            foreach (IFormFile file in Request.Form.Files)
            {
                string fullPath = Path.Combine(uploadPath, $"{r.Next()}{Path.GetExtension(file.FileName)}");

                using FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
            }
            return Ok();
        }
    }
}
