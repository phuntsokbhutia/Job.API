using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Domain.Entities
{
    public class users
    {
        [Key]
        [Required]
        public int user_id { get; set; }
        [Required]
        public string asp_user_id { get; set; } = string.Empty;
        [Required]
        public string user_name { get; set; } = string.Empty;
        [Required]
        public string email_address { get; set; } = string.Empty;

    }
}
