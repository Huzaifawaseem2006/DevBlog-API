using System.ComponentModel.DataAnnotations;

namespace DevBlog.Core.Dtos
{
    public class UpdateCommentDto
    {
        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; }
        
    }
}
