using DevBlog.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DevBlog.Core.Dtos;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace DevBlog.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(Guid id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            return Ok(comment);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _commentService.GetAllCommentsAsync();
            return Ok(comments);
        }

        [HttpGet("post/{postId}")]
        public async Task<IActionResult> GetCommentsByPostId(Guid postId)
        {
            var comments = await _commentService.GetCommentsByPostIdAsync(postId);
            return Ok(comments);
        }

        [HttpPost("post/{postId}")]
        [Authorize]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentDto createCommentDto,Guid postId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var authorId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var comment = await _commentService.CreateCommentAsync(createCommentDto, authorId, postId);
            return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateComment(Guid id, [FromBody] UpdateCommentDto updateCommentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var authorId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var isAdmin = User.IsInRole("Admin");
            await _commentService.UpdateCommentAsync(updateCommentDto, id, authorId,isAdmin);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            var authorId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var isAdmin = User.IsInRole("Admin");
            await _commentService.DeleteCommentAsync(id, authorId, isAdmin);
            return NoContent();
        }
    }
}
