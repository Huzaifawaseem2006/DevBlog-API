using DevBlog.Core.Entities;
using DevBlog.Core.Helpers;

namespace DevBlog.Core.Interfaces
{
    public interface IPostRepository: IRepository<Post>
    {
       // Additional methods specific to Post can be defined here
       Task<List<Post>> GetPostsByAuthorAsync(Guid authorId);
       Task<PagedResult<Post>> GetAllPostsPaginatedAsync(int pageNumber, int pageSize);
    }
}
