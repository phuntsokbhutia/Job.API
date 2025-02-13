using Job.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Application.Interface
{
    public interface IJobService
    {
        Task<APIResponseDTO> AddJobAsync(JobsDTO dto);
        Task<APIResponseDTO> GetAllJobsAsync();
        Task<APIResponseDTO> GetJobByIdAsync(int id);
        Task<APIResponseDTO> ApplyJobAsync(JobApplyDTO dto);
        Task<APIResponseDTO> GetJobDetailsByUser(int userId);
        Task<APIResponseDTO> UpdateJobApplicationStatus(UpdateJobStatusDTO dto);

    }
}
