using System.ComponentModel.DataAnnotations;

namespace DevBlog.Core.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;

    }
}
