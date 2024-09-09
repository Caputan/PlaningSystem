using ProjectPlanningSystem.Model;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.Extensions.Options;

namespace ProjectPlanningSystem
{
    public class PPSContext : DbContext
    {
        public DbSet<User> Users { get; set; } 
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Project> Projects { get; set; }

        public string DbPath { get; }

        public PPSContext()
        {
            // Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=ppsystem.db");
    }
}
