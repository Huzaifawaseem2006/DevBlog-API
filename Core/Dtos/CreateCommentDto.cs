using System.ComponentModel.DataAnnotations;

namespace DevBlog.Core.Dtos
{
    public class CreateCommentDto
    {
        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; } = string.Empty;

    }
}
