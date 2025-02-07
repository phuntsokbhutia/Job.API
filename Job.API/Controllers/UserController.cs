using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Job.Infrastructure.Services;
using Job.Application.Interface;
using Job.Application.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Job.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<APIResponseDTO> RegisterUser([FromBody] RegisterUserDTO dto)
        {
           return await _userService.RegisterUserAsync(dto);

        }
        // POST: api/User/Login
        [HttpPost("Login")]

        public async Task<APIResponseDTO> LoginUser([FromBody] LoginDTO dto)
        {
            // Call the service method to login user
           return  await _userService.LoginUserAsync(dto);
            
          
        }
        [HttpGet("GetUsers")]

        public async Task<APIResponseDTO> GetAllUsers()
        {
            return await _userService.GetAllUserAsync();
        }

          [HttpPost("AddJobs")]

        public async Task<APIResponseDTO> Addjobs(JobsDTO jobsDTO)
        {
            return await _userService.AddJobAsync(jobsDTO);
        }




    }
}

    



