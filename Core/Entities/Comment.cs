using System;


namespace DevBlog.Core.Entities
{
    public class Comment:BaseEntity
    {
        public string Content { get; set; } = string.Empty;

        public Guid AuthorId { get; set; }

        public ApplicationUser Author { get; set; }
        // Foreign key to the post
        public Guid PostId { get; set; }
        // Navigation property to the post
        public Post Post { get; set; }
    }
}
