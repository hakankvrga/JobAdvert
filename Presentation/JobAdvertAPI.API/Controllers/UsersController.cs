using JobAdvertAPI.Aplication.Repositories;
using JobAdvertAPI.Domain.Entities;
using JobAdvertAPI.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace JobAdvertAPI.API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        readonly private IUserWriteRepository _userWriteRepository;
        readonly private IUserReadRepository _userReadRepository;

        public UsersController(IUserWriteRepository userWriteRepository, IUserReadRepository userReadRepository)
        {
            _userWriteRepository = userWriteRepository;
            _userReadRepository = userReadRepository;
        }


        //[HttpGet]
        //public async Task Get() {

        //    User user = await _userReadRepository.GetByIdAsync(12);
        //    user.FirstName = "ahmet";
        //    await _userWriteRepository.SaveAsync();
        //}
        [HttpGet]
        public async Task Get()
        {
            await _userWriteRepository.AddRangeAsync(new()
   {
         new(){ FirstName="sdsgdsg", LastName="asdgsag", ContactNumber="sagasg", Cv="gsaggs", Email="asgsagg", Password="afsfag", UserTypeId=1 }
         
     });
            var count = await _userWriteRepository.SaveAsync();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
           User user= await _userReadRepository.GetByIdAsync(id);
            
            return Ok(user);
        }
    }
}
