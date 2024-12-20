using NotesManagement.Api.Entities;
using NotesManagement.Api.Repositories.Interfaces;
using NotesManagement.Api.Services.Interfaces;
using System.Linq;

namespace NotesManagement.Api.Repositories.Implementations
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IIOService<User> _ioService;
        private readonly ITokenService _tokenService;

        public AuthRepository(IIOService<User> ioService, ITokenService tokenService)
        {
            _ioService = ioService;
            _tokenService = tokenService;
        }

        // Helper method to validate user authentication
        private void EnsureUserAuthenticated()
        {
            return;
            //later for admin
            if (_tokenService.GetUserIdFromToken() == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            EnsureUserAuthenticated();

            var entities = await _ioService.ReadDataAsync();
            return entities?.FirstOrDefault(e => e.Id == id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            EnsureUserAuthenticated();

            var entities = await _ioService.ReadDataAsync();
            return entities ?? Enumerable.Empty<User>();  // Handle potential null return
        }

        public async Task AddAsync(User entity)
        {
            EnsureUserAuthenticated();

            var entities = await _ioService.ReadDataAsync() ?? new List<User>(); // Handle null if no users are found
            entity.UserId = entity.Email;  // Ensure this is intended logic
            entity.Id = entities.Any() ? entities.Max(e => e.Id) + 1 : 1;
            entities.Add(entity);
            await _ioService.WriteDataAsync(entities);
        }

        public async Task UpdateAsync(User entity)
        {
            EnsureUserAuthenticated();

            var entities = await _ioService.ReadDataAsync();
            var existingEntity = entities?.FirstOrDefault(e => e.Id == entity.Id);

            if (existingEntity != null)
            {
                entities.Remove(existingEntity);
                entities.Add(entity);
                await _ioService.WriteDataAsync(entities);
            }
            else
            {
                throw new ArgumentException("User not found to update.");
            }
        }

        public async Task DeleteAsync(int id)
        {
            EnsureUserAuthenticated();

            var entities = await _ioService.ReadDataAsync();
            var entity = entities?.FirstOrDefault(e => e.Id == id);

            if (entity != null)
            {
                entities.Remove(entity);
                await _ioService.WriteDataAsync(entities);
            }
            else
            {
                throw new ArgumentException("User not found to delete.");
            }
        }
    }
}
