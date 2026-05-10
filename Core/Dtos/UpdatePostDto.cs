namespace DevBlog.Core.Dtos
{
    public class UpdatePostDto
    {
        public string Title { get; set; }
        public string Content { get; set; } = string.Empty;
        public List<Guid> TagIds { get; set; } = new List<Guid>();
    }
}
