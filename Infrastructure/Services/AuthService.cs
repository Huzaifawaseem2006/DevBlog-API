using DevBlog.Core.Interfaces;
using DevBlog.Core.Dtos;
using DevBlog.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;


namespace DevBlog.Infrastructure.Services
{
    public class AuthService: IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(ITokenService tokenService, UserManager<ApplicationUser> userManager)
        {
            _tokenService = tokenService;
            _userManager = userManager;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto model)
        {
            ApplicationUser user = new ApplicationUser
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email,
                CreatedAt = DateTime.UtcNow

            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var authResponse = _tokenService.GenerateToken(user);
                user.RefreshToken = authResponse.RefreshToken;
                user.RefreshTokenExpiration = authResponse.RefreshTokenExpiration;
                await _userManager.UpdateAsync(user);
                return authResponse;
            }
            throw new InvalidOperationException(
                 string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto model)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Email == model.Email);
            if (user == null)
            {
                throw new Exception("Invalid email or password.");
            }
            bool isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!isPasswordValid)
            {
                throw new Exception("Invalid email or password.");
            }
            var authResponse = _tokenService.GenerateToken(user);
            user.RefreshToken = authResponse.RefreshToken;
            user.RefreshTokenExpiration = authResponse.RefreshTokenExpiration;
            await _userManager.UpdateAsync(user);
            return authResponse;
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(TokensDto tokenModel)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(tokenModel.Token);
            if (principal == null)
            {
                throw new Exception("Invalid token.");
            }
            // Get the user ID from the token claims
            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _userManager.Users.FirstOrDefault(u => u.Id.ToString() == userId);
            // Validate the refresh token
            if (user == null || user.RefreshToken != tokenModel.RefreshToken || user.RefreshTokenExpiration <= DateTime.UtcNow)
            {
                throw new Exception("Invalid token.");
            }
            var authResponse = _tokenService.GenerateToken(user);
            user.RefreshToken = authResponse.RefreshToken;
            user.RefreshTokenExpiration = authResponse.RefreshTokenExpiration;
            await _userManager.UpdateAsync(user);
            return authResponse;

        }
    }
}
