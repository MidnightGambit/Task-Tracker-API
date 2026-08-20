using Microsoft.EntityFrameworkCore;
using TaskTrackerApi.Models;

namespace TaskTrackerApi.Data
{
    // Wraps the underlying SQL database (SQLite here; swap the provider
    // in Program.cs to target SQL Server with no other code changes).
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TaskItem> Tasks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>()
                .HasIndex(t => t.Title);

            base.OnModelCreating(modelBuilder);
        }
    }
}
