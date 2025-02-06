using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Application.DTOs
{
    public class APIResponseDTO
    {
        public string status { get; set; } 
        public string message { get; set; }
        public object? data { get; set; }
    }
    public class LoginDTO
    {
        public string email { get; set; }
        public string password { get; set; }
    }

}
