namespace DevBlog.Core.Dtos
{
    public class CommentDetailsDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

    }
}
