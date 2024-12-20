using NotesManagement.Api.DTOs;
using System.Security.Claims;

namespace NotesManagement.Api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthenticatedUserDto> RegisterAsync(RegisterDto signupDto);
        Task<AuthenticatedUserDto> LoginAsync(LoginDto loginDto);
        Task LogoutAsync();
    }

}
