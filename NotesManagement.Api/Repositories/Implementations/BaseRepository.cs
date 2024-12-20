using NotesManagement.Api.Entities;
using NotesManagement.Api.Repositories.Interfaces;
using NotesManagement.Api.Services.Interfaces;

namespace NotesManagement.Api.Repositories.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly IIOService<T> _ioService;
        private readonly ITokenService _tokenService;

        public BaseRepository(IIOService<T> ioService, ITokenService tokenService)
        {
            _ioService = ioService;
            _tokenService = tokenService;
        }

        private int? GetUserIdFromToken()
        {
            return _tokenService.GetUserIdFromToken();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var entities = await _ioService.ReadDataAsync();
            return entities.FirstOrDefault(e => e.Id == id && e.UserId == userId);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var entities = await _ioService.ReadDataAsync();
            return entities.Where(e => e.UserId == userId).OrderByDescending(i=>i.CreatedAt);
        }

        public async Task<T> AddAsync(T entity)
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var entities = await _ioService.ReadDataAsync();
            entity.UserId = userId.Value;
            entity.Id = Guid.NewGuid().ToString();
            entity.CreatedAt = DateTime.UtcNow;
            entities.Add(entity);
            await _ioService.WriteDataAsync(entities);
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var entities = await _ioService.ReadDataAsync();
            var existingEntity = entities.FirstOrDefault(e => e.Id == entity.Id && e.UserId == userId);

            if (existingEntity != null)
            {
                entities.Remove(existingEntity);
                entity.Id = existingEntity.Id;
                entity.UserId = existingEntity.UserId;
                entity.CreatedAt = existingEntity.CreatedAt;
                entity.UpdatedAt = DateTime.UtcNow;
                entities.Add(entity);
                await _ioService.WriteDataAsync(entities);
            }
            return entity;
        }

        public async Task DeleteAsync(string id)
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var entities = await _ioService.ReadDataAsync();
            var entity = entities.FirstOrDefault(e => e.Id == id && e.UserId == userId);

            if (entity != null)
            {
                entities.Remove(entity);
                await _ioService.WriteDataAsync(entities);
            }
        }
    }
}
