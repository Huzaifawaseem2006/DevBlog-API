using DevBlog.Core.Helpers;

namespace DevBlog.Core.Interfaces
{
    public interface ILikeService
    {
        public Task<Result<bool>> LikePostAsync(Guid postId, Guid userId);
        public Task<Result<bool>> UnlikePostAsync(Guid postId, Guid userId);
        public Task<Result<int>> GetLikesCountAsync(Guid postId);
    }
}
