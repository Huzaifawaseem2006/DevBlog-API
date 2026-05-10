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


        public async Task<CommentDetailsDto> GetCommentByIdAsync(Guid id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null)
            {
                throw new Exception($"Comment with id {id} not found.");
            }
            var commentDetailsDto = CommentMapping.ToCommentDetailsDto(comment);
            return commentDetailsDto;

        }

        public async Task<IEnumerable<CommentDetailsDto>> GetAllCommentsAsync()
        {
            var comments = await _commentRepo.GetAllAsync();
            return comments.Select(CommentMapping.ToCommentDetailsDto);
        }

        public async Task<CommentDetailsDto> CreateCommentAsync(CreateCommentDto createCommentDto, Guid authorId, Guid postId)
        {
            var comment = CommentMapping.ToComment(createCommentDto, authorId, postId);
            await _commentRepo.AddAsync(comment);
            var savedComment = await _commentRepo.GetByIdAsync(comment.Id);
            var commentDetailsDto = CommentMapping.ToCommentDetailsDto(savedComment);
            return commentDetailsDto;
        }

        public async Task UpdateCommentAsync(UpdateCommentDto comment, Guid commentId, Guid authorId,bool isAdmin)
        {
            var existingComment = await _commentRepo.GetByIdAsync(commentId);
            if (existingComment == null)
            {
                throw new Exception($"Comment with id {commentId} not found.");
            }
            if (existingComment.AuthorId != authorId && !isAdmin)
            {
                throw new Exception("You are not authorized to update this comment.");
            }
            var updatedComment = CommentMapping.ToComment(comment, existingComment);
            await _commentRepo.UpdateAsync(updatedComment);

        }

        public async Task DeleteCommentAsync(Guid id, Guid authorId,bool isAdmin)
        {
            var existingComment = await _commentRepo.GetByIdAsync(id);
            if (existingComment == null)
            {
                throw new Exception($"Comment with id {id} not found.");
            }
            if (existingComment.AuthorId != authorId && !isAdmin)
            {
                throw new Exception("You are not authorized to delete this comment.");
            }
            await _commentRepo.DeleteAsync(existingComment);
        }

        public async Task<IEnumerable<CommentDetailsDto>> GetCommentsByPostIdAsync(Guid postId)
        {
            var comments = await _commentRepo.GetCommentsByPostIdAsync(postId);
            return comments.Select(CommentMapping.ToCommentDetailsDto);
        }
    }
}
