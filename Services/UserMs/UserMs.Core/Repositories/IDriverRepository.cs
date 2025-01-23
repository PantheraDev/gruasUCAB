using UserMs.Domain.Entities;

namespace UserMs.Core.Repositories{
    public interface IDriverRepository {
        Task<List<Driver>> GetDriverAsync();
        Task<Driver?> GetDriverById(UserId userId);
        Task AddAsync(Driver driver);
        Task<Driver?> UpdateDriverAsync(UserId userId, Driver driver);
        Task<Driver?> DeleteDriverAsync(UserId userId);
    }
}