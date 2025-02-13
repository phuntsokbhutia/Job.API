using Job.Domain.Entities;
using Job.Infrastructure.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet <users> users { get; set; }
        public DbSet<user_job_apply> user_job_apply { get; set; }
        public DbSet<job_details> job_details { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define Foreign Key Relationships
            modelBuilder.Entity<user_job_apply>()
                .HasOne(a => a.users)
                .WithMany()
                .HasForeignKey(a => a.user_id)
                .OnDelete(DeleteBehavior.NoAction); // Delete applies if user is deleted

            modelBuilder.Entity<user_job_apply>()
                .HasOne(a => a.job_details)
                .WithMany()
                .HasForeignKey(a => a.job_id)
                .OnDelete(DeleteBehavior.NoAction); // Delete applies if job is deleted

            // Ensure (UserId, JobId) combination is UNIQUE
            modelBuilder.Entity<user_job_apply>()
                .HasIndex(a => new { a.job_id, a.user_id })
                .IsUnique();
        }
    }
    
}