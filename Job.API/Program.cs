using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Job.Infrastructure.Data;
using Job.Application.Interface;
using Job.Infrastructure.Services.Job.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); 
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();

//Register DI sevices
builder.Services.AddScoped<IUserService, UserService>();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();  // Adds API Explorer (required for Swagger)
builder.Services.AddSwaggerGen();  // Adds Swagger generator

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  // Expose the Swagger JSON endpoint
    app.UseSwaggerUI();  // Expose the Swagger UI for easy testing
}

app.UseAuthorization();

app.MapControllers();

app.Run();
