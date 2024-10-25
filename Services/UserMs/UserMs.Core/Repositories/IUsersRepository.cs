using UserMs.Domain.Entities;

namespace UserMs.Core.Repositories{
    public interface IUsersRepository {
        Task<List<Users>> GetUsersAsync();
        Task<Users?> GetUsersById(UserId userId);
        Task AddAsync(Users users);
        Task<Users?> UpdateUsersAsync(UserId userId, Users users);
        Task<Users?> DeleteUsersAsync(UserId userId);
    }
}