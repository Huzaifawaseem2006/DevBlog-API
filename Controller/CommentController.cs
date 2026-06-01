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
        public async Task<IActionResult> GetCommentById(Guid id,CancellationToken token)
        {
            var result = await _commentService.GetCommentByIdAsync(id,token);
            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments(CancellationToken token)
        {
            var result = await _commentService.GetAllCommentsAsync(token);
            return Ok(result.Value);
        }

        [HttpGet("post/{postId}")]
        public async Task<IActionResult> GetCommentsByPostId(Guid postId,CancellationToken token)
        {
            var result = await _commentService.GetCommentsByPostIdAsync(postId,token);
            return Ok(result.Value);
        }

        [HttpPost("post/{postId}")]
        [Authorize]
        public async Task<IActionResult> CreateComment(CancellationToken token,[FromBody] CreateCommentDto createCommentDto,Guid postId)
        {
            
            var authorId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _commentService.CreateCommentAsync(createCommentDto, authorId, postId,token);
            if(!result.Success)
            {
                return BadRequest(result.Error);
            }
            return CreatedAtAction(nameof(GetCommentById), new { id = result.Value.Id }, result.Value);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateComment(Guid id, [FromBody] UpdateCommentDto updateCommentDto,CancellationToken token)
        {
           
            var authorId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var isAdmin = User.IsInRole("Admin");
            var result = await _commentService.UpdateCommentAsync(updateCommentDto, id, authorId,isAdmin,token);
            if(!result.Success)
            {
                return BadRequest(result.Error);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteComment(Guid id,CancellationToken token)
        {
            var authorId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var isAdmin = User.IsInRole("Admin");
            var result = await _commentService.DeleteCommentAsync(id, authorId, isAdmin,token);
            if(!result.Success)
            {
                return BadRequest(result.Error);
            }
            return NoContent();
        }
    }
}
