using DevBlog.Core.Interfaces;
using DevBlog.Core.Entities;
using DevBlog.Core.Dtos;
using DevBlog.Core.Helpers;
namespace DevBlog.Infrastructure.Services
{
    public class TagService : ITagService
    {

        private readonly ITagRepository _tagRepo;
        public TagService(ITagRepository tagRepository)
        {
            _tagRepo = tagRepository;
        }


        public async Task<IEnumerable<TagDetailsDto>> GetAllTagsAsync()
        {
            var tags = await _tagRepo.GetAllAsync();
            return tags.Select(TagMapping.ToDto);
        }

        public async Task<TagDetailsDto> GetTagByIdAsync(Guid id)
        {
            var tag = await _tagRepo.GetByIdAsync(id);
            if (tag == null)
            {
                throw new Exception($"Tag with id {id} not found.");
            }
            return TagMapping.ToDto(tag);
        }

        public async Task<TagDetailsDto> CreateTagAsync(CreateTagDto createTagDto)
        {
            var tag = TagMapping.ToTag(createTagDto);
            await _tagRepo.AddAsync(tag);
            return TagMapping.ToDto(tag);
        }

        public async Task UpdateTagAsync(UpdateTagDto updateTagDto, Guid tagId)
        {
            var existingTag = await _tagRepo.GetByIdAsync(tagId);
            if (existingTag == null)
            {
                throw new Exception($"Tag with id {tagId} not found.");
            }
            var updatedTag = TagMapping.ToUpdatedDto(updateTagDto, existingTag);
            await _tagRepo.UpdateAsync(updatedTag);
        }

        public async Task DeleteTagAsync(Guid id)
        {
            var existingTag = await _tagRepo.GetByIdAsync(id);
            if (existingTag == null)
            {
                throw new Exception($"Tag with id {id} not found.");
            }
            await _tagRepo.DeleteAsync(existingTag);

        }

        public async Task<IEnumerable<TagDetailsDto>> GetTagsByPostIdAsync(Guid postId)
        {
            var tags = await _tagRepo.GetTagsByPostIdAsync(postId);
            return tags.Select(TagMapping.ToDto);

        }
    }
}
