using DevBlog.Core.Interfaces;
using DevBlog.Core.Entities;
using DevBlog.Core.Dtos;
using DevBlog.Core.Helpers;

namespace DevBlog.Infrastructure.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepo;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepo = commentRepository;
        }


        public async Task<Result<CommentDetailsDto>> GetCommentByIdAsync(Guid id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null)
            {
                return Result<CommentDetailsDto>.Fail($"Comment with id {id} not found.");
            }
            var commentDetailsDto = CommentMapping.ToCommentDetailsDto(comment);
            return Result<CommentDetailsDto>.Ok(commentDetailsDto);

        }

        public async Task<Result<IEnumerable<CommentDetailsDto>>> GetAllCommentsAsync()
        {
            var comments = await _commentRepo.GetAllAsync();
            var commentDtos = comments.Select(CommentMapping.ToCommentDetailsDto);
            return Result<IEnumerable<CommentDetailsDto>>.Ok(commentDtos);
        }

        public async Task<Result<CommentDetailsDto>> CreateCommentAsync(CreateCommentDto createCommentDto, Guid authorId, Guid postId)
        {
            var comment = CommentMapping.ToComment(createCommentDto, authorId, postId);
            await _commentRepo.AddAsync(comment);
            var savedComment = await _commentRepo.GetByIdAsync(comment.Id);
            var commentDetailsDto = CommentMapping.ToCommentDetailsDto(savedComment);
            return Result<CommentDetailsDto>.Ok(commentDetailsDto);
        }

        public async Task<Result<bool>> UpdateCommentAsync(UpdateCommentDto comment, Guid commentId, Guid authorId,bool isAdmin)
        {
            var existingComment = await _commentRepo.GetByIdAsync(commentId);
            if (existingComment == null)
            {
                return Result<bool>.Fail($"Comment with id {commentId} not found.");
            }
            if (existingComment.AuthorId != authorId && !isAdmin)
            {
                return Result<bool>.Fail("You are not authorized to update this comment.");
            }
            var updatedComment = CommentMapping.ToComment(comment, existingComment);
            await _commentRepo.UpdateAsync(updatedComment);
            return Result<bool>.Ok(true);
        }

        public async Task<Result<bool>> DeleteCommentAsync(Guid id, Guid authorId,bool isAdmin)
        {
            var existingComment = await _commentRepo.GetByIdAsync(id);
            if (existingComment == null)
            {
                return Result<bool>.Fail($"Comment with id {id} not found.");
            }
            if (existingComment.AuthorId != authorId && !isAdmin)
            {
                return Result<bool>.Fail("You are not authorized to delete this comment.");
            }
            await _commentRepo.DeleteAsync(existingComment);
            return Result<bool>.Ok(true);
        }

        public async Task<Result<IEnumerable<CommentDetailsDto>>> GetCommentsByPostIdAsync(Guid postId)
        {
            var comments = await _commentRepo.GetCommentsByPostIdAsync(postId);
            var commentDtos = comments.Select(CommentMapping.ToCommentDetailsDto);
            return Result<IEnumerable<CommentDetailsDto>>.Ok(commentDtos);
        }
    }
}
