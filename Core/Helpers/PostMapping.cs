using DevBlog.Core.Dtos;
using DevBlog.Core.Entities;

namespace DevBlog.Core.Helpers
{
    public static class PostMapping
    {
        public static PostDetailsDto ToPostDetailsDto(this Post post)
        {
            return new PostDetailsDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                Slug = post.Slug,
                AuthorName = post.Author?.DisplayName ?? "Unknown",
                CreatedAt = post.CreatedAt
            };
        }

        public static Post ToPost(this CreatePostDto createPostDto, Guid authorId)
        {
            return new Post
            {
                Id = Guid.NewGuid(),
                Title = createPostDto.Title,
                Content = createPostDto.Content,
                AuthorId = authorId,
                Slug = createPostDto.Title.ToLower().Replace(" ", "-"),
                CreatedAt = DateTime.UtcNow
            };
        }

        public static Post ToPost(this UpdatePostDto updatePostDto, Post existingPost)
        {
            existingPost.Title = updatePostDto.Title;
            existingPost.Content = updatePostDto.Content;
            existingPost.Slug = updatePostDto.Title.ToLower().Replace(" ", "-");
            existingPost.UpdatedAt = DateTime.UtcNow;

            return existingPost;
        }
    }
}
