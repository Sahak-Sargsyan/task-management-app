using Microsoft.EntityFrameworkCore;
using TaskDAL.Entities;

namespace TaskDAL
{
    public class TaskContext : DbContext
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<Entities.Task>? Tasks { get; set; }
        public DbSet<Category>? Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // the actual connection string should be hidden, this is just a fake connection string for now
            optionsBuilder.UseSqlServer(
                @"Server=dev;Database=dev;User Id=dev;Password=dev;");
        }

        // Configuring the tables using Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // one-to-many relationship between User and Task
            modelBuilder.Entity<User>()
                .HasMany(u => u.Tasks)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);

            // one-to-many relationship between Category and Task
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Tasks)
                .WithOne(t => t.Category)
                .HasForeignKey(t => t.CategoryId);
        }
    }
}
