using Microsoft.AspNetCore.Mvc;
using DevBlog.Core.Dtos;

using DevBlog.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DevBlog.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTagsAsync(CancellationToken token)
        {
            var result = await _tagService.GetAllTagsAsync(token);
            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        [ActionName("GetTagById")]
        public async Task<IActionResult> GetTagByIdAsync(Guid id, CancellationToken token)
        {
            var result = await _tagService.GetTagByIdAsync(id, token);
            return Ok(result.Value);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateTagAsync([FromBody] CreateTagDto createTagDto, CancellationToken token)
        {

            var result = await _tagService.CreateTagAsync(createTagDto, token);
            return CreatedAtAction("GetTagById", new { id = result.Value.Id }, result.Value);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTagAsync(Guid id, [FromBody] UpdateTagDto updateTagDto, CancellationToken token)
        {

            var result = await _tagService.UpdateTagAsync(updateTagDto, id, token);
            if (!result.Success)
            {
                return BadRequest(result.Error);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTagAsync(Guid id, CancellationToken token)
        {
            var result = await _tagService.DeleteTagAsync(id, token);
            if (!result.Success)
            {
                return BadRequest(result.Error);
            }
            return NoContent();
        }

        [HttpGet("post/{postId}")]
        public async Task<IActionResult> GetTagsByPostIdAsync(Guid postId, CancellationToken token)
        {
            var result = await _tagService.GetTagsByPostIdAsync(postId, token);
            return Ok(result.Value);

        }
    }
}
