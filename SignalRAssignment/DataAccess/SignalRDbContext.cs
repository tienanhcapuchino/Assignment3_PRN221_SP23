using Microsoft.EntityFrameworkCore;
using SignalRAssignment.Configuration;
using SignalRAssignment.Entity;

namespace SignalRAssignment.DataAccess
{
    public class SignalRDbContext : DbContext
    {
        public SignalRDbContext(DbContextOptions<SignalRDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PostsConfiguration());
            modelBuilder.ApplyConfiguration(new PostsCategoriesConfiguration());
            modelBuilder.ApplyConfiguration(new AppUsersConfiguration());
        }

        #region Entity
        public DbSet<AppUsers> AppUsers { get; set; }
        public DbSet<PostCategories> PostCategories { get; set; }
        public DbSet<Posts> Posts { get; set; }

        #endregion
    }
}
