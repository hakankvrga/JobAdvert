using JobAdvertAPI.Aplication.Abstractions.Storage;
using JobAdvertAPI.Aplication.Repositories;
using JobAdvertAPI.Aplication.RequestParameters;

using JobAdvertAPI.Aplication.ViewModels.JobPosts;
using JobAdvertAPI.Aplication.ViewModels.Users;
using JobAdvertAPI.Domain.Entities;
using JobAdvertAPI.Persistence.Repositories;
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
        readonly private IJobPostWriteRepository _jobPostWriteRepository;
        readonly private IJobPostReadRepository _jobPostReadRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
       
        readonly IFileWriteRepository _fileWriteRepository;
        readonly IFileReadRepository _fileReadRepository;
        readonly IJobPostImageFileReadRepository _jobPostImageFileReadRepository;
        readonly IJobPostImageFileWriteRepository _jobPostImageFileWriteRepository;
        readonly IStorageService _storageService;
        readonly IConfiguration configuration;
        public JobPostsController(IJobPostWriteRepository jobPostWriteRepository, IJobPostReadRepository jobPostReadRepository, IWebHostEnvironment webHostEnvironment, IFileWriteRepository fileWriteRepository, IFileReadRepository fileReadRepository, IJobPostImageFileReadRepository jobPostImageFileReadRepository, IJobPostImageFileWriteRepository jobPostImageFileWriteRepository, IStorageService storageService, IConfiguration configuration)
        {
            _jobPostWriteRepository = jobPostWriteRepository;
            _jobPostReadRepository = jobPostReadRepository;
            _webHostEnvironment = webHostEnvironment;

            _fileWriteRepository = fileWriteRepository;
            _fileReadRepository = fileReadRepository;
            _jobPostImageFileReadRepository = jobPostImageFileReadRepository;
            _jobPostImageFileWriteRepository = jobPostImageFileWriteRepository;
            _storageService = storageService;
            this.configuration = configuration;
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
        public async Task<IActionResult> Upload(int id)
        {
            List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("photo-images", Request.Form.Files);


          JobPost jobPost= await   _jobPostReadRepository.GetByIdAsync(id);

          await  _jobPostImageFileWriteRepository.AddRangeAsync(result.Select(r => new JobPostImageFile
            {
                FileName=r.fileName,
                Path= r.pathOrContainerName,
                Storage= _storageService.StorageName,
                JobPosts =new List<JobPost>() { jobPost}
            }).ToList());

            await _jobPostImageFileWriteRepository.SaveAsync();
            return Ok();
        }


        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetJobPostImages(int id)
        {
          JobPost? jobPost=  await  _jobPostReadRepository.Table.Include(j => j.JobPostImageFiles).FirstOrDefaultAsync(j=> j.Id == id);
            return Ok(jobPost.JobPostImageFiles.Select(j => new
            {
                Path= $"{configuration["BaseStoragaUrl"]}/{j.Path}",
                j.FileName,
                j.Id
                
            })); 
        }


        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteJobPostImage(int id, int imageId)
        {
            JobPost? jobPost = await _jobPostReadRepository.Table.Include(j => j.JobPostImageFiles).FirstOrDefaultAsync(j => j.Id == id);

            JobPostImageFile jobPostImageFile = jobPost.JobPostImageFiles.FirstOrDefault(j => j.Id == imageId);
            jobPost.JobPostImageFiles.Remove(jobPostImageFile);
            await _jobPostWriteRepository.SaveAsync();
            return Ok();
        }
    }
}
