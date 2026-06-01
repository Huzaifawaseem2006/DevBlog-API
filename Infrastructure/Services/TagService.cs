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


        public async Task<Result<IEnumerable<TagDetailsDto>>> GetAllTagsAsync(CancellationToken token)
        {
            var tags = await _tagRepo.GetAllAsync(token);
            return Result<IEnumerable<TagDetailsDto>>.Ok(tags.Select(TagMapping.ToDto));
        }

        public async Task<Result<TagDetailsDto>> GetTagByIdAsync(Guid id, CancellationToken token)
        {
            var tag = await _tagRepo.GetByIdAsync(id, token);
            if (tag == null)
            {
                return Result<TagDetailsDto>.Fail($"Tag with id {id} not found.");
            }
            return Result<TagDetailsDto>.Ok(TagMapping.ToDto(tag));
        }

        public async Task<Result<TagDetailsDto>> CreateTagAsync(CreateTagDto createTagDto, CancellationToken token)
        {
            var tag = TagMapping.ToTag(createTagDto);
            await _tagRepo.AddAsync(tag, token);
            return Result<TagDetailsDto>.Ok(TagMapping.ToDto(tag));
        }

        public async Task<Result<bool>> UpdateTagAsync(UpdateTagDto updateTagDto, Guid tagId, CancellationToken token)
        {
            var existingTag = await _tagRepo.GetByIdAsync(tagId, token);
            if (existingTag == null)
            {
                return Result<bool>.Fail($"Tag with id {tagId} not found.");
            }
            var updatedTag = TagMapping.ToUpdatedDto(updateTagDto, existingTag);
            await _tagRepo.UpdateAsync(updatedTag, token);
            return Result<bool>.Ok(true);
        }

        public async Task<Result<bool>> DeleteTagAsync(Guid id, CancellationToken token)
        {
            var existingTag = await _tagRepo.GetByIdAsync(id, token);
            if (existingTag == null)
            {
                return Result<bool>.Fail($"Tag with id {id} not found.");
            }
            await _tagRepo.DeleteAsync(existingTag, token);
            return Result<bool>.Ok(true);

        }

        public async Task<Result<IEnumerable<TagDetailsDto>>> GetTagsByPostIdAsync(Guid postId, CancellationToken token)
        {
            var tags = await _tagRepo.GetTagsByPostIdAsync(postId, token);
            return Result<IEnumerable<TagDetailsDto>>.Ok(tags.Select(TagMapping.ToDto));

        }
    }
}
