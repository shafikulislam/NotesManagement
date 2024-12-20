using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using NotesManagement.Api.Entities;
using NotesManagement.Api.Repositories.Interfaces;
using NotesManagement.Api.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NotesManagement.Api.Services.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TokenService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // Generate JWT Token
        public string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("N56%$$32927hhdg6DGS&*2kkkdd@56Hjh$$32927hhdg6DGS&*2kkkdd00"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "Shafikul Islam",
                audience: "Insightin Technology",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Get User ID from Token

        public int? GetUserIdFromToken(ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : (int?)null;
        }

        public int? GetUserIdFromToken()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user != null ? GetUserIdFromToken(user) : (int?)null;
        }

    }
}
