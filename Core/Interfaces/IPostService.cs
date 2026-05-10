using DevBlog.Core.Dtos;
using DevBlog.Core.Entities;
using DevBlog.Core.Helpers;



namespace DevBlog.Core.Interfaces
{
    public interface IPostService
    {
            Task<PostDetailsDto> GetPostByIdAsync(Guid id);
            Task<IEnumerable<PostDetailsDto>> GetAllPostsAsync();
            Task<PostDetailsDto> CreatePostAsync(CreatePostDto post, Guid authorId);
            Task UpdatePostAsync(UpdatePostDto post, Guid postId, Guid authorId);
            Task DeletePostAsync(Guid id, Guid authorId, bool isAdmin);

            Task<IEnumerable<PostDetailsDto>> GetPostByAuthorIdAsync(Guid authorId);

            Task<PagedResult<PostDetailsDto>> GetAllPostsPaginatedAsync(int pageNumber, int pageSize);
    }
}
