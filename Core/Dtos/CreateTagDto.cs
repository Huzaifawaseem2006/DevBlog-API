using System.ComponentModel.DataAnnotations;

namespace DevBlog.Core.Dtos
{
    public class CreateTagDto
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;

    }
}
