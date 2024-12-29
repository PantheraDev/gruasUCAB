//TODO: Borrar todos los console, los using innecesarios y los comentarios innecesarios
using Microsoft.EntityFrameworkCore;
using OrderMs.Common.Exceptions;
using OrderMs.Core.Database;
using OrderMs.Core.Repositories;
using OrderMs.Domain.Entities;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Infrastructure.Repositories
{
    public class AdditionalCostRepository : IAdditionalCostRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public AdditionalCostRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddAsync(AdditionalCost additionalCost)
        {
            await _dbContext.AdditionalCosts.AddAsync(additionalCost);
            await _dbContext.SaveEfContextChanges("");
        }

        public async Task<AdditionalCost?> GetByIdAsync(AdditionalCostId id)
        {
            var additionalCost = await _dbContext.AdditionalCosts.FirstOrDefaultAsync(x => x.Id == id);
            return additionalCost;
        }
        public async Task<List<AdditionalCost>?> GetAllAsync()
        {
            var additionalCosts = await _dbContext.AdditionalCosts.ToListAsync();
            return additionalCosts;
        }

        public async Task DeleteAsync(AdditionalCostId id)
        {
            var additionalCost = await _dbContext.AdditionalCosts.FirstOrDefaultAsync(x => x.Id == id);
            if (additionalCost == null) throw new AdditionalCostNotFoundException("AdditionalCost not found");

            additionalCost.IsDeleted = true;
            await _dbContext.SaveEfContextChanges("");
        }

        public async Task<AdditionalCost?> UpdateAsync(AdditionalCost additionalCost)
        {
            _dbContext.AdditionalCosts.Update(additionalCost);
            await _dbContext.SaveEfContextChanges("");
            return additionalCost;
        }

        public Task<bool> ExistsAsync(AdditionalCostId id)
        {
            return _dbContext.AdditionalCosts.AnyAsync(x => x.Id == id);
        }
    }
}