using DevBlog.Core.Dtos;
using DevBlog.Core.Entities;

namespace DevBlog.Core.Helpers
{
    public static class CommentMapping
    {
        public static CommentDetailsDto ToCommentDetailsDto(this Comment comment)
        {
            

            return new CommentDetailsDto
            {
                Id = comment.Id,
                Content = comment.Content,
                AuthorName = comment.Author?.DisplayName ?? "Unknown",
                CreatedAt = comment.CreatedAt,
                UpdatedAt = comment.UpdatedAt
            };
        }

        public static Comment ToComment(this CreateCommentDto createCommentDto, Guid authorId, Guid postId)
        {
            return new Comment
            {
                Id = Guid.NewGuid(),
                Content = createCommentDto.Content,
                AuthorId = authorId,
                PostId = postId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        public static Comment ToComment(this UpdateCommentDto updateCommentDto, Comment existingComment)
        {
            existingComment.Content = updateCommentDto.Content;
            existingComment.UpdatedAt = DateTime.UtcNow;

            return existingComment;
        }
    }
}
