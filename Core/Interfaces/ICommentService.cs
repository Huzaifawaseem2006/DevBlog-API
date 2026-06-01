using DevBlog.Core.Entities;
using DevBlog.Core.Dtos;
using DevBlog.Core.Helpers;

namespace DevBlog.Core.Interfaces
{
    public interface ICommentService
    {
            Task<Result<CommentDetailsDto>> GetCommentByIdAsync(Guid id,CancellationToken token);
            Task<Result<IEnumerable<CommentDetailsDto>>> GetCommentsByPostIdAsync(Guid postId,CancellationToken token);

            Task<Result<IEnumerable<CommentDetailsDto>>> GetAllCommentsAsync(CancellationToken token);
            Task<Result<CommentDetailsDto>> CreateCommentAsync(CreateCommentDto comment, Guid authorId, Guid postId,CancellationToken token);
            Task<Result<bool>> UpdateCommentAsync(UpdateCommentDto comment, Guid commentId, Guid authorId,bool isAdmin,CancellationToken token);
            Task<Result<bool>> DeleteCommentAsync(Guid id, Guid authorId,bool isAdmin,CancellationToken token);

    }
}
