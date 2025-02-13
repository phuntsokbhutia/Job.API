using Job.Application.DTOs;
using Job.Application.Interface;
using Job.Infrastructure.Services.Job.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Job.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpPost("AddJobs")]
        public async Task<APIResponseDTO> Addjobs(JobsDTO jobsDTO)
        {
            return await _jobService.AddJobAsync(jobsDTO);
        }
        [HttpGet("GetAllJobs")]
        public async Task<APIResponseDTO> GetJobs()
        {
            return await _jobService.GetAllJobsAsync();
        }
        [HttpGet("GetJobById")]
        public async Task<APIResponseDTO> GetJobById(int id)
        {
            return await _jobService.GetJobByIdAsync(id);
        }
        [HttpPost("ApplyJob")]
        public async Task<APIResponseDTO> ApplyJobAsync( JobApplyDTO dto)
        {
            return await _jobService.ApplyJobAsync(dto);
        }
        [HttpGet("GetJobDetailsByUserId")]
        public async Task<APIResponseDTO> GetJobDetailsByUser(int userId)
        {
            return await _jobService.GetJobDetailsByUser(userId);
        }
        [HttpPut("UpdateJobApplicationStatus")]
        public async Task<APIResponseDTO> UpdateJobApplicationStatus(UpdateJobStatusDTO dto)
        {
            return await _jobService.UpdateJobApplicationStatus(dto);
        }
    }
}
