using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NotesManagement.Api.DTOs;
using NotesManagement.Api.Entities;
using NotesManagement.Api.Repositories.Interfaces;
using NotesManagement.Api.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NotesManagement.Api.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IAuthRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        // Register a new user
        public async Task<AuthenticatedUserDto> RegisterAsync(RegisterDto userDto)
        {
            var users = await _userRepository.GetAllAsync();

            // Check if the username already exists
            if (users.Any(u => u.Email == userDto.Email))
            {
                throw new Exception("Username is already taken.");
            }

            // Hash the password before storing
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

            var user = new User
            {
                Id = users.Count() > 0 ? users.Max(u => u.Id) + 1 : 1,
                Name = userDto.Name,
                DateOfBirth = userDto.DateOfBirth,
                UserId = userDto.Email,
                Email = userDto.Email,
                HashedPassword = hashedPassword,
            };

            await _userRepository.AddAsync(user);

            return new AuthenticatedUserDto
            {
                Token = _tokenService.GenerateJwtToken(user),
                Name = userDto.Name,
                DateOfBirth = userDto.DateOfBirth,
                Email = userDto.Email,
                Password = "",
            };
        }

        // Login a user and return the token
        public async Task<AuthenticatedUserDto> LoginAsync(LoginDto userDto)
        {
            var users = await _userRepository.GetAllAsync();
            var user = users.FirstOrDefault(u => u.Email == userDto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(userDto.Password, user.HashedPassword))
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            return new AuthenticatedUserDto
            {
                Token = _tokenService.GenerateJwtToken(user),
                Name = user.Name,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                Password = "",
            };
        }

        // Logout (Just a placeholder as JWT is stateless)
        public Task LogoutAsync()
        {
            // JWTs don't need to be "logged out", the client just removes the token
            return Task.CompletedTask;
        }
    }

}
