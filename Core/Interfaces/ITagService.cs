using DevBlog.Core.Entities;
using DevBlog.Core.Dtos;

namespace DevBlog.Core.Interfaces
{
    public interface ITagService
    {
        public Task<IEnumerable<TagDetailsDto>> GetAllTagsAsync();
        public Task<TagDetailsDto> GetTagByIdAsync(Guid id);
        public Task<TagDetailsDto> CreateTagAsync(CreateTagDto createTagDto);

        public Task UpdateTagAsync(UpdateTagDto updateTagDto,Guid id);

        public Task DeleteTagAsync(Guid id);

        public Task<IEnumerable<TagDetailsDto>> GetTagsByPostIdAsync(Guid postId);
    }
}
