//TODO: Borrar todos los console, los using innecesarios y los comentarios innecesarios
using Microsoft.EntityFrameworkCore;
using OrderMs.Common.Exceptions;
using OrderMs.Core.Database;
using OrderMs.Core.Repositories;
using OrderMs.Domain.Entities;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Infrastructure.Repositories
{
    public class FeeRepository : IFeeRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public FeeRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddAsync(Fee fee)
        {
            await _dbContext.Fees.AddAsync(fee);
            await _dbContext.SaveEfContextChanges("");
        }

        public async Task<Fee?> GetByIdAsync(FeeId id)
        {
            var fee = await _dbContext.Fees.FirstOrDefaultAsync(x => x.Id == id);
            //TODO: Borrar todos los console
            Console.WriteLine("This is the Fee:" + fee);
            return fee;
        }
        public async Task<List<Fee>?> GetAllAsync()
        {
            var fees = await _dbContext.Fees.ToListAsync();
            return fees;
        }

        public async Task DeleteAsync(FeeId id)
        {
            var fee = await _dbContext.Fees.FirstOrDefaultAsync(x => x.Id == id);
            if (fee == null) throw new FeeNotFoundException("Fee not found");

            fee.IsDeleted = true;
            await _dbContext.SaveEfContextChanges("");
        }

        public async Task<Fee?> UpdateAsync(Fee fee)
        {
            _dbContext.Fees.Update(fee);
            await _dbContext.SaveEfContextChanges("");
            return fee;
        }

        public Task<bool> ExistsAsync(FeeId id)
        {
            return _dbContext.Fees.AnyAsync(x => x.Id == id);
        }
    }
}