using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Application.DTOs
{

    public class JobsDTO
    {

        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        public string JobType { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Salary { get; set; }

        [Required]
        [EmailAddress]
        public string ContactEmail { get; set; }

        [Required]
        [Phone]
        public string ContactPhone { get; set; }

        [Required]
        [StringLength(200)]
        public string Location { get; set; }

        [Required]
        [StringLength(200)]
        public string CompanyName { get; set; }

        [StringLength(500)]
        public string CompanyDescription { get; set; }
    }
    public class GetJobsDTO
    {
        public int JobId { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Salary { get; set; }

        [Required]
        [EmailAddress]
        public string ContactEmail { get; set; }

        [Required]
        [Phone]
        public string ContactPhone { get; set; }

        [Required]
        [StringLength(200)]
        public string Location { get; set; }

        [Required]
        [StringLength(200)]
        public string CompanyName { get; set; }

        [StringLength(500)]
        public string CompanyDescription { get; set; }
    }
    public class JobApplyDTO
    {
        public int UserId { get; set; }
        public int JobId { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public string Skills { get; set; }
    }
    public class UserJobApplyDetailsDTO
    {
        public int JobId { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
    }
    public class AllJobApplyDetailsDTO
    {
        public int JobId { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }


    }
    public class UpdateJobStatusDTO
    {
        public int JobId { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
    }
}

