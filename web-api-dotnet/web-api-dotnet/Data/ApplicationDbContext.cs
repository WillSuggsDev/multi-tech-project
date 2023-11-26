using Microsoft.EntityFrameworkCore;
using web_api_dotnet.Models;

namespace web_api_dotnet.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<BlogPost> BlogPosts { get; set; }
    }
}
