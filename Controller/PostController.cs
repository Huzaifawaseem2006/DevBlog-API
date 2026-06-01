using System.Security.Claims;
using DevBlog.Core.Dtos;
using DevBlog.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DevBlog.Core.Helpers;


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
        public async Task<IActionResult> GetAllPosts(CancellationToken token,[FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _postService.GetAllPostsPaginatedAsync(page, pageSize, token);
            if (!result.Success)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(Guid id,CancellationToken token)
        {
            var result = await _postService.GetPostByIdAsync(id, token);
            if (!result.Success)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostDto createPostDto,CancellationToken token)
        {
            
            var authorId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _postService.CreatePostAsync(createPostDto, authorId, token);
            if (!result.Success)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(nameof(GetPostById), new { id = result.Value.Id }, result.Value);
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdatePost(Guid id, [FromBody] UpdatePostDto updatePostDto,CancellationToken token)
        {
          
            var authorId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _postService.UpdatePostAsync(updatePostDto, id, authorId, token);
            if (!result.Success)
            {
                return BadRequest(result.Error);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePost(Guid id,CancellationToken token)
        {
            var authorId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var isAdmin = User.IsInRole("Admin");
            var result = await _postService.DeletePostAsync(id, authorId, isAdmin, token);
            if (!result.Success)
            {
                return BadRequest(result.Error);
            }
            return NoContent();

        }

        [HttpGet("author/{authorId}")]
        public async Task<IActionResult> GetPostsByAuthorId(Guid authorId,CancellationToken token)
        {
            var result = await _postService.GetPostByAuthorIdAsync(authorId, token);
            if (!result.Success)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchPostsAsync([FromQuery] string searchTerm,CancellationToken token)
        {
            var result = await _postService.SearchPostsAsync(searchTerm, token);
            if (!result.Success)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);

        }

    }
}
