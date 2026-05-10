

namespace DevBlog.Core.Entities
{
    public class Post : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;
        // Foreign key to the author (ApplicationUser)
        public Guid AuthorId { get; set; }
        // Navigation property to the author (ApplicationUser)
        public ApplicationUser Author { get; set; }

        // Navigation property to the comments
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public ICollection<Tag> Tags { get; set; } = new List<Tag>();   

    }
}
