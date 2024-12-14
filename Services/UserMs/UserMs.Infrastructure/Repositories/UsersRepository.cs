using UserMs.Core.Repositories;
using UserMs.Domain.Entities;
using UserMs.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using UserMs.Core.Database;

namespace UserMs.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IUserDbContext _dbContext;

        public UsersRepository(IUserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Users>> GetUsersAsync()
        {
            var users = new List<Users>();
            await foreach (var user in _dbContext.Users)
            {
                users.Add(user);
            }
            return users;
        }

        public async Task<Users?> GetUsersById(UserId userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        public async Task AddAsync(Users users)
        {
            await _dbContext.Users.AddAsync(users);
            await _dbContext.SaveEfContextChanges("");
        }

        public async Task<Users?> UpdateUsersAsync(UserId userId, Users users)
        {
            var existingUsers = await _dbContext.Users.FindAsync(userId);
            await _dbContext.SaveEfContextChanges("");
            return existingUsers;
        }

        public async Task<Users?> DeleteUsersAsync(UserId userId)
        {
            var existingUsers = await _dbContext.Users.FindAsync(userId);
            await _dbContext.SaveEfContextChanges("");
            return existingUsers;
        }
    }
}