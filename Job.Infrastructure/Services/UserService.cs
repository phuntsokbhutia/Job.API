﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Job.Application.DTOs;
using Job.Application.Interface;
using Job.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Job.Domain.Entities;

namespace Job.Infrastructure.Services
{
    namespace Job.Infrastructure.Services
    {
        public class UserService : IUserService
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IConfiguration _configuration;
            private readonly AppDbContext _appDbContext;

            public UserService(UserManager<ApplicationUser> userManager,
                               IConfiguration configuration, AppDbContext appDbContext)
            {
                _userManager = userManager;
                _configuration = configuration;
                _appDbContext = appDbContext;
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

                var user =
                    new ApplicationUser { UserName = dto.user_name, Email = dto.email };

                var result = await _userManager.CreateAsync(user, dto.password);

                if (result.Succeeded)
                {
                    // ✅ Assign the "User" role to every new registered user
                    await _userManager.AddToRoleAsync(user, "User");

                    return new APIResponseDTO
                    {
                        status = "Success",
                        message = "User registered successfully ",
                    };
                }

                return new APIResponseDTO
                {
                    status = "Error",
                    message = "Error registering user. Please try again later.",
                };
            }

            public async Task<APIResponseDTO> LoginUserAsync(LoginDTO dto)
            {
                // Check if the user exists by email
                var user = await _userManager.FindByEmailAsync(dto.email);
                if (user == null)
                {
                    return new APIResponseDTO
                    {
                        status = "Error",
                        message = "Invalid email address or password."
                    };
                }

                // Check if the provided password is correct
                var isPasswordValid =
                    await _userManager.CheckPasswordAsync(user, dto.password);
                if (!isPasswordValid)
                {
                    return new APIResponseDTO
                    {
                        status = "Error",
                        message = "Invalid email address or password."
                    };
                }

                // Get user roles
                var roles = await _userManager.GetRolesAsync(user);

                // Generate JWT token
                var token = GenerateJwtToken(user);

                return new APIResponseDTO
                {
                    status = "Success",
                    message = "Login successful.",
                    data = new { aspuserId = user.Id, email = user.Email, token = token, roles = roles }
                };
            }

            private string GenerateJwtToken(ApplicationUser user)
            {
                // Create claims for the token
                var claims = new[] { new Claim(ClaimTypes.NameIdentifier, user.Id),
                             new Claim(ClaimTypes.Email, user.Email) };

                // Get the secret key from configuration and create signing credentials
                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // Create the token descriptor
                var tokenDescriptor = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"], claims: claims,
                    expires: DateTime.UtcNow.AddDays(
                        7),  // Adjust token expiration as needed
                    signingCredentials: creds);

                // Generate the JWT token string
                return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            }

            public async Task<APIResponseDTO> GetAllUserAsync()
            {
                // Ensure you use the correct DbContext and the ApplicationUser type.
                var users =
                    await _userManager.Users
                        .AsNoTracking()  // AsNoTracking is used here because it's a
                                         // query for data and we don't need to track
                                         // entities.
                        .Select(u => new GetUserDTO
                        {
                            id = u.Id,  // Ensure to use the correct casing: "Id" instead
                                        // of "id"
                            user_name = u.UserName,  // "UserName" is the correct property
                                                     // name in ASP.NET Core Identity.
                            email = u.Email  // "Email" is the correct property name in
                                             // ASP.NET Core Identity.
                        })
                        .ToListAsync();

                // Check if no users were found
                if (users == null || !users.Any())
                {
                    return new APIResponseDTO
                    {
                        status = "Success",
                        message = "No users found."
                    };
                }

                return new APIResponseDTO
                {
                    status = "Success",
                    message = "Users data retrieved successfully.",
                    data = users  // Return the list of users here.
                };
            }

            public async Task<APIResponseDTO> AddJobAsync(JobsDTO jobsDTO)
            {
                // Map JobsDTO to Job entity
                var job = new job_details
                {
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
                _appDbContext.Jobs.Add(job);

                try
                {
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

























        }
    }
}