//TODO: Borrar todos los console, los using innecesarios y los comentarios innecesarios
using Microsoft.EntityFrameworkCore;
using OrderMs.Common.Exceptions;
using OrderMs.Core.Database;
using OrderMs.Core.Repositories;
using OrderMs.Domain.Entities;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Infrastructure.Repositories
{
    public class PolicyRepository : IPolicyRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public PolicyRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddAsync(Policy policy)
        {
            await _dbContext.Policies.AddAsync(policy);
            await _dbContext.SaveEfContextChanges("");
        }

        public async Task<Policy?> GetByIdAsync(PolicyId id)
        {
            var policy = await _dbContext.Policies.FirstOrDefaultAsync(x => x.Id == id);
            //TODO: Borrar todos los console
            Console.WriteLine("This is the Policy:" + policy);
            return policy;
        }
        public async Task<List<Policy>?> GetAllAsync()
        {
            var policies = await _dbContext.Policies.ToListAsync();
            return policies;
        }

        public async Task DeleteAsync(PolicyId id)
        {
            var policy = await _dbContext.Policies.FirstOrDefaultAsync(x => x.Id == id);
            if (policy == null) throw new PolicyNotFoundException("Policy not found");

            policy.IsDeleted = true;
            await _dbContext.SaveEfContextChanges("");
        }

        public async Task<Policy?> UpdateAsync(Policy policy)
        {
            _dbContext.Policies.Update(policy);
            await _dbContext.SaveEfContextChanges("");
            return policy;
        }

        public Task<bool> ExistsAsync(PolicyId id)
        {
            return _dbContext.Policies.AnyAsync(x => x.Id == id);
        }
    }
}