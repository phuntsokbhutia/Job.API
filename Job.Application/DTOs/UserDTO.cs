using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Application.DTOs
{
    public class RegisterUserDTO
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 100 characters.")]
        public string user_name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required.")]
        [Compare("password", ErrorMessage = "Passwords do not match.")]
        public string confirm_password { get; set; }
    }


    public class GetUserDTO
    {
        public string id { get; set; }
        public string user_name { get; set; }
        public string email { get; set; }

    }



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
        public string UserId { get; set; }
        public int JobId { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public string Skills { get; set; }
    }




}
