namespace DevBlog.Core.Dtos
{
    public class CreatePostDto
    {
        
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public List<Guid> TagIds { get; set; } = new List<Guid>();

    }
}
