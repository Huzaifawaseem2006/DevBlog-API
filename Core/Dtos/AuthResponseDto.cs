namespace DevBlog.Core.Dtos
{
    public class AuthResponseDto
    {
        public string? PersonName { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
        public DateTime Expiration { get; set; } 

        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
