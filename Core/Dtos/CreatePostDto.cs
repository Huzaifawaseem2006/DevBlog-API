using System.ComponentModel.DataAnnotations;

namespace DevBlog.Core.Dtos
{
    public class CreatePostDto
    {
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; } = string.Empty;
        [Required(ErrorMessage = "TagIds are required.")]
        public List<Guid> TagIds { get; set; } = new List<Guid>();

    }
}
