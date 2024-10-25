using UserMs.Core.Repositories;
using UserMs.Domain.Entities;
using UserMs.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using UserMs.Core.Database;

namespace UserMs.Infrastructure.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly IUserDbContext _dbContext;

        public DriverRepository(IUserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Driver>> GetDriverAsync()
        {
            var drivers = new List<Driver>();
            await foreach (var driver in _dbContext.Drivers)
            {
                drivers.Add(driver);
            }
            return drivers;
        }

        public async Task<Driver?> GetDriverById(UserId userId)
        {
            return await _dbContext.Drivers.FindAsync(userId);
        }

        public async Task AddAsync(Driver driver)
        {
            await _dbContext.Drivers.AddAsync(driver);
            await _dbContext.SaveEfContextChanges("");
        }

        public async Task<Driver?> UpdateDriverAsync(UserId userId, Driver driver)
        {
            var existingDriver = await _dbContext.Drivers.FindAsync(userId);
            await _dbContext.SaveEfContextChanges("");
            return existingDriver;
        }

        public async Task<Driver?> DeleteDriverAsync(UserId userId)
        {
            var existingDriver = await _dbContext.Drivers.FindAsync(userId);
            await _dbContext.SaveEfContextChanges("");
            return existingDriver;
        }
    }
}