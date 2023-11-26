using web_api_dotnet.Data;
using web_api_dotnet.Models;

namespace web_api_dotnet.Services
{
    public class BlogService
    {
        private readonly ApplicationDbContext _dbContext;

        public BlogService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<BlogPost> GetAllPost()
        {
            return _dbContext.BlogPosts.ToList();
        }

        public void AddPost(BlogPost post)
        {
            post.CreatedAt = DateTime.Now;
            _dbContext.BlogPosts.Add(post);
            _dbContext.SaveChanges();
        }


        public BlogPost GetPostById(int id)
        {
            return _dbContext.BlogPosts.Find(id);
        }

        public void UpdatePost(int id, BlogPost updatedPost)
        {
            var existingPost = _dbContext.BlogPosts.Find(id);

            if (existingPost != null)
            {
                existingPost.Title = updatedPost.Title;
                existingPost.Content = updatedPost.Content;
                _dbContext.SaveChanges();
            }
        }

        public void DeletePost(int id)
        {
            var postToDelete = _dbContext.BlogPosts.Find(id);

            if (postToDelete != null)
            {
                _dbContext.BlogPosts.Remove(postToDelete);
                _dbContext.SaveChanges();
            }
        }
    }
}
