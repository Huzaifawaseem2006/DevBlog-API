using DevBlog.Core.Entities;
using DevBlog.Core.Helpers;

namespace DevBlog.Core.Interfaces
{
    public interface IPostRepository: IRepository<Post>
    {
       // Additional methods specific to Post can be defined here
       Task<List<Post>> GetPostsByAuthorAsync(Guid authorId, CancellationToken token);
       Task<PagedResult<Post>> GetAllPostsPaginatedAsync(int pageNumber, int pageSize, CancellationToken token);

        Task<List<Post>> SearchPostsAsync(string searchterm, CancellationToken token);

        Task LikePostAsync(Post post, ApplicationUser user, CancellationToken token);
        Task UnlikePostAsync(Post post, ApplicationUser user, CancellationToken token);
    }
}
