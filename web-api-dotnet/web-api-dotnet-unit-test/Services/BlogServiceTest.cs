using Microsoft.EntityFrameworkCore;
using web_api_dotnet.Data;
using web_api_dotnet.Models;
using web_api_dotnet.Services;

namespace web_api_dotnet_unit_test.Services
{
    public class BlogServiceTest
    {


        [Test]
        public void GetAllPost_ReturnsCorrectCount()
        {
            // Arrange
            var options = CreateDbContextOptions("InMemoryDatabase_GetAllPost");

            using (var context = new ApplicationDbContext(options))
            {
                context.BlogPosts.Add(new BlogPost { Title = "Post 1", Content = "Content 1" });
                context.BlogPosts.Add(new BlogPost { Title = "Post 2", Content = "Content 2" });
                context.SaveChanges();

                var blogService = new BlogService(context);

                // Act
                var post = blogService.GetAllPost();

                // Assert
                Assert.AreEqual(2, post.Count());
            }
        }

        [Test]
        public void AddPost_IncreasesPostCount() 
        {
            // Arrange 
            var options = CreateDbContextOptions("InMemoryDatabase_AddPost");

            using (var context = new ApplicationDbContext(options))
            {
                var blogService = new BlogService(context);

                // Act
                blogService.AddPost(new BlogPost { Title = "New Post", Content = "New Content" });

                // Assert
                Assert.AreEqual(1, context.BlogPosts.Count());
            }
        }

        [Test]
        public void GetPostById_ReturnsCorrectPost()
        {
            // Arrange
            var options = CreateDbContextOptions("InMemoryDatabase_GetPostById");

            using(var context = new ApplicationDbContext(options))
            {
                context.BlogPosts.Add(new BlogPost { Id = 1, Title = "Post 1", Content = "Content 1" });
                context.SaveChanges();

                var blogService = new BlogService(context);

                // Act
                var post = blogService.GetPostById(1);

                // Assert
                Assert.IsNotNull(post);
                Assert.AreEqual("Post 1", post.Title);
            }
        }

        [Test]
        public void UpdatePost_UpdatesPost()
        {
            // Arrange 
            var options = CreateDbContextOptions("InMemoryDatabase_UpdatePost");

            using (var context = new ApplicationDbContext(options))
            {
                context.BlogPosts.Add(new BlogPost { Id = 1, Title = "Post 1", Content = "Content 1" });
                context.SaveChanges();

                var blogService = new BlogService(context);

                //Act
                blogService.UpdatePost(1, new BlogPost { Title = "Updated Post", Content = "Updated Content" });

                // Assert
                var updatedPost = context.BlogPosts.Find(1);
                Assert.IsNotNull(updatedPost);
                Assert.AreEqual("Updated Post", updatedPost.Title);
            }
        }

        [Test]
        public void DeletePost_RemovesPost() 
        {
            // Arrange
            var options = CreateDbContextOptions("InMemoryDatabase_DeletePost");

            using (var context = new ApplicationDbContext(options))
            {
                context.BlogPosts.Add(new BlogPost { Id = 1, Title = "Post 1", Content = "Content 1" });
                context.SaveChanges();

                var blogService = new BlogService(context);

                // Act
                blogService.DeletePost(1);

                // Assert
                Assert.AreEqual(0, context.BlogPosts.Count());
            }
        }

        private DbContextOptions<ApplicationDbContext> CreateDbContextOptions(string databaseName)
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }
    }
}
