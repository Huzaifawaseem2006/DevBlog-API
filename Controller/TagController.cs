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
        public async Task<IActionResult> GetAllTagsAsync()
        {
            var tags = await _tagService.GetAllTagsAsync();
            return Ok(tags);

        }

        [HttpGet("{id}")]
        [ActionName("GetTagById")]
        public async Task<IActionResult> GetTagByIdAsync(Guid id)
        {
            var tag = await _tagService.GetTagByIdAsync(id);
            return Ok(tag);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateTagAsync([FromBody] CreateTagDto createTagDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tag = await _tagService.CreateTagAsync(createTagDto);
            return CreatedAtAction("GetTagById", new { id = tag.Id }, tag);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTagAsync(Guid id, [FromBody] UpdateTagDto updateTagDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _tagService.UpdateTagAsync(updateTagDto, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTagAsync(Guid id)
        {
            await _tagService.DeleteTagAsync(id);
            return NoContent();
        }

        [HttpGet("post/{postId}")]
        public async Task<IActionResult> GetTagsByPostIdAsync(Guid postId)
        {
            var tags = await _tagService.GetTagsByPostIdAsync(postId);
            return Ok(tags);

        }
    }
}
