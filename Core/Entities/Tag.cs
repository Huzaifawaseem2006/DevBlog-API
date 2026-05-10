namespace DevBlog.Core.Entities
{
    public class Tag:BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
