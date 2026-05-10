using System.Security.Claims;
using DevBlog.Core.Dtos;
using DevBlog.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace DevBlog.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPosts([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var posts = await _postService.GetAllPostsPaginatedAsync(page, pageSize);
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            return Ok(post);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostDto createPostDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var authorId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var post = await _postService.CreatePostAsync(createPostDto, authorId);
            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdatePost(Guid id, [FromBody] UpdatePostDto updatePostDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var authorId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            await _postService.UpdatePostAsync(updatePostDto, id, authorId);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var authorId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var isAdmin = User.IsInRole("Admin");
            await _postService.DeletePostAsync(id, authorId,isAdmin);
            return NoContent();

        }

        [HttpGet("author/{authorId}")]
        public async Task<IActionResult> GetPostsByAuthorId(Guid authorId)
        {
            var posts = await _postService.GetPostByAuthorIdAsync(authorId);
            return Ok(posts);
        }

    }
}
