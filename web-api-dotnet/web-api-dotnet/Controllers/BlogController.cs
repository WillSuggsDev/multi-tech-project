using Microsoft.AspNetCore.Mvc;
using web_api_dotnet.Models;
using web_api_dotnet.Services;

namespace web_api_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly BlogService _blogService;

        public BlogController(BlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public IActionResult GetAllPost()
        {
            var post = _blogService.GetAllPost();
            return Ok(post);
        }

        [HttpGet("{id}")]
        public IActionResult GetPostById(int id)
        {
            var post = _blogService.GetPostById(id);

            if (post == null) return NotFound();

            return Ok(post);
        }

        [HttpPost]
        public IActionResult AddPost([FromBody] BlogPost post)
        {
            _blogService.AddPost(post);
            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePost(int id, [FromBody] BlogPost updatedPost)
        {
            _blogService.UpdatePost(id, updatedPost);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePost(int id)
        {
            _blogService.DeletePost(id);
            return NoContent();
        }
    }
}
