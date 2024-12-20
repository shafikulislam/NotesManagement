using NotesManagement.Api.Entities;

namespace NotesManagement.Api.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User entity);
        Task UpdateAsync(User entity);
        Task DeleteAsync(int id);
    }

}
