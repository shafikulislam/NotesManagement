using NotesManagement.Api.Entities;
using System.Security.Claims;

namespace NotesManagement.Api.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user);
        int? GetUserIdFromToken(ClaimsPrincipal user);
        int? GetUserIdFromToken();
    }
}
