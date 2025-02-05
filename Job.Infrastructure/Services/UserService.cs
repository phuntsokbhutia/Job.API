using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Job.Application.DTOs;
using Job.Application.Interface;
using Job.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Job.Infrastructure.Services
{
    namespace Job.Infrastructure.Services
    {
        public class UserService : IUserService
        {
            private readonly UserManager<ApplicationUser> _userManager;

            public UserService(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<APIResponseDTO> RegisterUserAsync(RegisterUserDTO dto)
            {
                var userCheck = await _userManager.FindByEmailAsync(dto.email);
                if (userCheck != null)
                {
                    return new APIResponseDTO
                    {
                        status = "Error",
                        message = "Email address already in use. Please use another email."
                    };
                }

                var user = new ApplicationUser
                {
                    UserName = dto.user_name,
                    Email = dto.email
                };

                var result = await _userManager.CreateAsync(user, dto.password);

                if (result.Succeeded)
                {
                    return new APIResponseDTO
                    {
                        status = "Success",
                        message = "User registered successfully.",
                    };
                }

                return new APIResponseDTO
                {
                    status = "Error",
                    message = "Error registering user.Please try again later.",
                };
            }


        }
    }
}
