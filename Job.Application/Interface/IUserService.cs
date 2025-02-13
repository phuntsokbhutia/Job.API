using Job.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Application.Interface
{
    public interface IUserService
    {
        Task<APIResponseDTO> RegisterUserAsync(RegisterUserDTO dto);
        Task<APIResponseDTO> LoginUserAsync(LoginDTO dto);
        Task<APIResponseDTO> GetAllUserAsync();
    }
}
