using System.ComponentModel.DataAnnotations;

namespace DevBlog.Core.Dtos
{
    public class UpdatePostDto
    {
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; } = string.Empty;
        [Required(ErrorMessage = "At least one tag is required.")]
        public List<Guid> TagIds { get; set; } = new List<Guid>();
    }
}
