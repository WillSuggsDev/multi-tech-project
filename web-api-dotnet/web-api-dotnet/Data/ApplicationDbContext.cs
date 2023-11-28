using Microsoft.EntityFrameworkCore;
using web_api_dotnet.Models;

namespace web_api_dotnet.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<BlogPost> BlogPosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogPost>().HasData(
                new BlogPost { Id = 1, Title = "Introduction to Docker", Content = "Docker is a powerful tool for containerization..." },
                new BlogPost { Id = 2, Title = "Getting Started with React", Content = "React is a JavaScript library for building user interfaces..." }
            );
        }
    }
}
