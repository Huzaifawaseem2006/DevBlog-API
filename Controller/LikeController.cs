using System.Security.Claims;
using System.Threading.Tasks;
using DevBlog.Core.Helpers;
using DevBlog.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DevBlog.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeService;
        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpPost("{postId}")]
        [Authorize]
        public async Task<IActionResult> LikePost(Guid postId)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userId == Guid.Empty)
            {
                return BadRequest(Result<bool>.Fail("User ID cannot be empty."));
            }
            var result = await _likeService.LikePostAsync(postId, userId);
            if (result.Success)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

        [HttpDelete("{postId}")]
        [Authorize]
        public async Task<IActionResult> UnlikePost(Guid postId)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userId == Guid.Empty)
            {
                return BadRequest(Result<bool>.Fail("User ID cannot be empty."));
            }
            var result = await _likeService.UnlikePostAsync(postId, userId);
            if (result.Success)
            {
                return Ok(result.Value);
            }
            return BadRequest((result.Error));
        }

        [HttpGet("{postId}/count")]

        public async Task<IActionResult> GetLikesCount(Guid postId)
        {
            var result = await _likeService.GetLikesCountAsync(postId);
            if (result.Success)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);

        }

    }

}