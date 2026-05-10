using DevBlog.Core.Entities;
using DevBlog.Core.Dtos;

namespace DevBlog.Core.Interfaces
{
    public interface ICommentService
    {
            Task<CommentDetailsDto> GetCommentByIdAsync(Guid id);
            Task<IEnumerable<CommentDetailsDto>> GetCommentsByPostIdAsync(Guid postId);

            Task<IEnumerable<CommentDetailsDto>> GetAllCommentsAsync();
            Task<CommentDetailsDto> CreateCommentAsync(CreateCommentDto comment, Guid authorId, Guid postId);
            Task UpdateCommentAsync(UpdateCommentDto comment, Guid commentId, Guid authorId,bool isAdmin);
            Task DeleteCommentAsync(Guid id, Guid authorId,bool isAdmin);

    }
}
