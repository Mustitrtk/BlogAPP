using BlogApp.API.Features.Auth;
using BlogApp.API.Features.Blog;
using BlogApp.API.Features.Category;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Reflection;

namespace BlogApp.API.Repository
{

    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Category> Categories{ get; set; }
        public DbSet<BlogEntity> Blogs { get; set; }
        public DbSet<UserEntity> User { get; set; }
        public static AppDbContext Create(IMongoDatabase database)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>().UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName);

            return new AppDbContext(optionsBuilder.Options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
