using Job.Application.DTOs;
using Job.Domain.Entities;
using Job.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Job.Application.Interface;
using System.Data;

namespace Job.Infrastructure.Services
{
    public class JobService:IJobService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _appDbContext;

        public JobService(UserManager<ApplicationUser> userManager,
                           IConfiguration configuration,
                           AppDbContext appDbContext)
        {
            _userManager = userManager;
            _configuration = configuration;
            _appDbContext = appDbContext;
        }
        public async Task<APIResponseDTO> AddJobAsync(JobsDTO jobsDTO)
        {
            try
            {
                // Map JobsDTO to Job entity
                var job = new job_details
            {
                JobType = jobsDTO.JobType,
                Title = jobsDTO.Title,
                Description = jobsDTO.Description,
                Salary = jobsDTO.Salary,
                ContactEmail = jobsDTO.ContactEmail,
                ContactPhone = jobsDTO.ContactPhone,
                Location = jobsDTO.Location,
                CompanyName = jobsDTO.CompanyName,
                CompanyDescription = jobsDTO.CompanyDescription,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Add the job to the DbContext
            _appDbContext.job_details.Add(job);

           
                // Save changes to the database asynchronously
                await _appDbContext.SaveChangesAsync();

                // Return a successful response
                return new APIResponseDTO
                {
                    status = "Success",
                    message = "Job added successfully."
                };
            }
            catch (Exception ex)
            {
                // Handle any database errors
                return new APIResponseDTO
                {
                    status = "Success",
                    message = $"Error adding job: {ex.Message}"
                };
            }
        }

        public async Task<APIResponseDTO> GetAllJobsAsync()
        {


            // Fetch jobs from the database
            var jobs = await _appDbContext.job_details.AsNoTracking()
                .Select(u => new GetJobsDTO
                {
                    JobId = u.JobId,
                    Title = u.Title,
                    Description = u.Description,
                    Salary = u.Salary,
                    ContactEmail = u.ContactEmail,
                    ContactPhone = u.ContactPhone,
                    Location = u.Location,
                    CompanyName = u.CompanyName,
                    CompanyDescription = u.CompanyDescription,
                })
           .ToListAsync();

            if (jobs == null)
            {
                return new APIResponseDTO
                {
                    status = "Information",
                    message = "No Data found.",
                };
            }

            // Create and return the API response
            return new APIResponseDTO
            {
                status = "Success",
                message = "Data retrieved successfully.",
                data = jobs  // Return the list of jobs here.
            };


        }
        public async Task<APIResponseDTO> GetJobByIdAsync(int id)
        {

            try
            {
                // Fetch jobs from the database
                var jobs = await _appDbContext.job_details.AsNoTracking().FirstOrDefaultAsync(w => w.JobId == id);

                if (jobs == null)
                {
                    return new APIResponseDTO
                    {
                        status = "Information",
                        message = "No job found with the ID provided.Verify the ID and try again.",
                    };
                }

                var jobdetails = new JobsDTO
                {

                    Title = jobs.Title,
                    Description = jobs.Description,
                    Salary = jobs.Salary,
                    ContactEmail = jobs.ContactEmail,
                    ContactPhone = jobs.ContactPhone,
                    Location = jobs.Location,
                    CompanyName = jobs.CompanyName,
                    CompanyDescription = jobs.CompanyDescription
                };

                // Create and return the API response
                return new APIResponseDTO
                {
                    status = "Success",
                    message = "Data retrieved successfully.",
                    data = jobdetails  // Return the list of jobs here.
                };
            }

            catch (Exception ex)
            {
                return new APIResponseDTO
                {
                    status = "Error",
                    message = $"Please try again later: {ex.Message} ",
                };

            }

        }

        public async Task<APIResponseDTO> ApplyJobAsync(JobApplyDTO dto)
        {
            // Validate user existence
            var user = await _appDbContext.users.FindAsync(dto.UserId); 
            if (user == null)
            {
                // Return failure response
                return new APIResponseDTO
                {
                    status = "Info",
                    message = "User not found.Verify the User Id."
                };
            }

            // Validate job existence
            var job = await _appDbContext.job_details.FindAsync(dto.JobId);
            if (job == null)
            {
                // Return failure response
                return new APIResponseDTO
                {
                    status = "Info",
                    message = "Job not found.Verify the Job Id."
                };
            }

            // Check if the user has already applied for this job
            var existingApplication = await _appDbContext.user_job_apply
                .FirstOrDefaultAsync(ua => ua.user_id == dto.UserId && ua.job_id == dto.JobId);
            if (existingApplication != null)
            {
                // Return failure response if the user has already applied for the job
                return new APIResponseDTO
                {
                    status = "Info",
                    message = "User has already applied for this job."
                };
            }

            // Create the job application record
            var application = new user_job_apply
            {
                user_id = dto.UserId,
                job_id = dto.JobId,
                phone_number = dto.PhoneNumber,
                description = dto.Description,
                skills = dto.Skills,
                status = "Pending", // Set initial status
                application_date = DateTime.UtcNow
            };

            // Add the application to the context and save changes to the database
            _appDbContext.user_job_apply.Add(application);
            await _appDbContext.SaveChangesAsync();

            // Return success response
            return new APIResponseDTO
            {
                status = "Success",
                message = "Job application submitted successfully!"
            };
        }
        public async Task<APIResponseDTO> GetJobDetailsByUser(int userId)
        {
            var userExists = await _appDbContext.users.AnyAsync(u => u.user_id==userId);

            // If the user does not exist
            if (!userExists)
            {
                return new APIResponseDTO
                {
                    status = "Info",
                    message = "User not found. Please ensure the user ID is correct."
                };
            }

            // Perform JOIN between user_job_apply and job_details tables
            var jobApplications = await (from ja in _appDbContext.user_job_apply
                                         join job in _appDbContext.job_details on ja.job_id equals job.JobId
                                         where ja.user_id == userId  // Filter by userId
                                         select new UserJobApplyDetailsDTO
                                         {
                                             JobId = ja.job_id,
                                             Status = ja.status,
                                             Title = job.Title
                                         }).ToListAsync();
            
            // Return the result wrapped in an APIResponseDTO
            return new APIResponseDTO
            {
                status = "Success",
                message = "Data retrieved successfully!",
                data = jobApplications
            };
        }

        public async Task<APIResponseDTO> GetAllJobDetails()
        {

            // Perform JOIN between user_job_apply and job_details tables
            var jobApplications = await (from ja in _appDbContext.user_job_apply
                                         join job in _appDbContext.job_details on ja.job_id equals job.JobId
                                         join user in _appDbContext.users on ja.user_id equals user.user_id
                                         select new AllJobApplyDetailsDTO
                                         {
                                             JobId = ja.job_id,
                                             Status = ja.status,
                                             Title = job.Title,
                                             UserId = user.user_id,
                                             UserName = user.user_name,
                                         }).ToListAsync();

            // Return the result wrapped in an APIResponseDTO
            return new APIResponseDTO
            {
                status = "Success",
                message = "Data retrieved successfully!",
                data = jobApplications
            };
        }

        public async Task<APIResponseDTO> UpdateJobApplicationStatus(UpdateJobStatusDTO dto)
        {
            // Verify if the user exists in the AspNetUsers table (user verification)
            var userExists = await _appDbContext.users.AnyAsync(u => u.user_id == dto.UserId);

            // If the user does not exist
            if (!userExists)
            {
                return new APIResponseDTO
                {
                    status = "Info",
                    message = "User not found. Please ensure the user ID is correct."
                };
            }

            // Find the job application entry for the specific user and job
            var jobApplication = await _appDbContext.user_job_apply
                .Where(ja => ja.user_id == dto.UserId && ja.job_id == dto.JobId)
                .FirstOrDefaultAsync();

            // If the job application doesn't exist for the user and job
            if (jobApplication == null)
            {
                return new APIResponseDTO
                {
                    status = "Info",
                    message = "Job application not found for the specified user and job."
                };
            }

            // Change the status to "Pending"
            jobApplication.status = dto.Status;

            // Save the changes to the database
            await _appDbContext.SaveChangesAsync();

            // Return a success response with the updated data
            return new APIResponseDTO
            {
                status = "Success",
                message = "Job application status updated.",
               
            };
        }

    }
}

