
using DevBlog.Core.Entities;

namespace DevBlog.Core.Interfaces
{
    public interface ITagRepository:IRepository<Tag>
    {
        public Task<IEnumerable<Tag>> GetTagsByPostIdAsync(Guid postId, CancellationToken token);
        public Task<IEnumerable<Tag>> GetTagsByIdAsync(IEnumerable<Guid> tagIds, CancellationToken token);

    }
}
