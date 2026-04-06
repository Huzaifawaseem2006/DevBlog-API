using System.Security.Claims;
using DevBlog.Core.Dtos;
using DevBlog.Core.Entities;
using Microsoft.AspNetCore.Authentication;

namespace DevBlog.Core.Interfaces
{
    public interface ITokenService
    {
        AuthResponseDto GenerateToken(ApplicationUser user);
        string GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
