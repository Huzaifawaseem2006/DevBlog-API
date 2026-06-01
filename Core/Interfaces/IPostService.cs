using DevBlog.Core.Dtos;
using DevBlog.Core.Entities;
using DevBlog.Core.Helpers;



namespace DevBlog.Core.Interfaces
{
    public interface IPostService
    {
        Task<Result<PostDetailsDto>> GetPostByIdAsync(Guid id, CancellationToken token);
        Task<Result<IEnumerable<PostDetailsDto>>> GetAllPostsAsync(CancellationToken token);
        Task<Result<PostDetailsDto>> CreatePostAsync(CreatePostDto post, Guid authorId, CancellationToken token);
        Task<Result<bool>> UpdatePostAsync(UpdatePostDto post, Guid postId, Guid authorId, CancellationToken token);
        Task<Result<bool>> DeletePostAsync(Guid id, Guid authorId, bool isAdmin, CancellationToken token);

        Task<Result<IEnumerable<PostDetailsDto>>> GetPostByAuthorIdAsync(Guid authorId, CancellationToken token);

        Task<Result<PagedResult<PostDetailsDto>>> GetAllPostsPaginatedAsync(int pageNumber, int pageSize, CancellationToken token);

        Task<Result<IEnumerable<PostDetailsDto>>> SearchPostsAsync(string searchTerm, CancellationToken token);

    }
}
