using Job.Domain.Entities;
using Job.Infrastructure.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Job.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        // Constructor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        // DbSet for Job entity
        public DbSet<job_details>Jobs{ get; set; }
        public DbSet<user_job_apply> user_job_apply { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define Foreign Key Relationships
            modelBuilder.Entity<user_job_apply>()
                .HasOne(a => a.User)
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
