using DevBlog.Core.Entities;

namespace DevBlog.Core.Interfaces
{
    public interface ICommentRepository:IRepository<Comment>
    {
        public Task<List<Comment>> GetCommentsByPostIdAsync(Guid postId);
    }
}
