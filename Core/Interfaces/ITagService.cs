using DevBlog.Core.Entities;
using DevBlog.Core.Dtos;
using DevBlog.Core.Helpers;

namespace DevBlog.Core.Interfaces
{
    public interface ITagService
    {
        public Task<Result<IEnumerable<TagDetailsDto>>> GetAllTagsAsync(CancellationToken token);
        public Task<Result<TagDetailsDto>> GetTagByIdAsync(Guid id, CancellationToken token);
        public Task<Result<TagDetailsDto>> CreateTagAsync(CreateTagDto createTagDto, CancellationToken token);

        public Task<Result<bool>> UpdateTagAsync(UpdateTagDto updateTagDto, Guid id, CancellationToken token);

        public Task<Result<bool>> DeleteTagAsync(Guid id, CancellationToken token);

        public Task<Result<IEnumerable<TagDetailsDto>>> GetTagsByPostIdAsync(Guid postId, CancellationToken token);
    }
}
