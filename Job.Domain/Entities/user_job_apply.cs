using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Domain.Entities
{
    public class user_job_apply
    {
        [Key]
        public int user_job_appply_id { get; set; }

        // Foreign Key for User (linked to users)
        public int user_id { get; set; }

        // Foreign Key for Job
        public int job_id { get; set; }
        public string status { get; set; }       // Status of the application (e.g., pending, accepted, rejected)
        public DateTime application_date { get; set; }  // Date when the application was submitted
        // Additional properties related to the application
        public string phone_number { get; set; } // User's phone number for the job application
        public string description { get; set; }  // Cover letter or description for the application
        public string skills { get; set; }       // Skills relevant to the job

        //nav properties

        public users users { get; set; }
        public job_details job_details { get; set; }


    }
}
