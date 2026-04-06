using DevBlog.Core.Dtos;

namespace DevBlog.Core.Interfaces
{
    public interface IAuthService
    {
            Task<AuthResponseDto> RegisterAsync(RegisterDto model);
            Task<AuthResponseDto> LoginAsync(LoginDto model);
            Task<AuthResponseDto> RefreshTokenAsync(TokensDto tokenModel);
    }
}
