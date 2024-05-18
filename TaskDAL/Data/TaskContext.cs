using Microsoft.EntityFrameworkCore;
using TaskDAL.Entities;

namespace TaskDAL
{
    /// <summary>
    /// DbContext class for Task management app
    /// </summary>
    public class TaskContext : DbContext
    {
        /// <summary>
        /// DbSet type of <see cref="User"/>
        /// </summary>
        public DbSet<User>? Users { get; set; }

        /// <summary>
        /// DbSet type of <see cref="Entities.Task"/>
        /// </summary>
        public DbSet<Entities.Task>? Tasks { get; set; }

        /// <summary>
        /// Initializes DbContext file for Task management app
        /// </summary>
        /// <param name="options"></param>
        public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }

        /// <summary>
        /// Configurations for Db Tables with fluent api
        /// </summary>
        /// <param name="modelBuilder">Model builder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Tasks)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);

        }
    }
}
