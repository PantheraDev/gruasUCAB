using Microsoft.EntityFrameworkCore;
using ProviderMs.Common.Exceptions;
using ProviderMs.Core.Database;
using ProviderMs.Core.Repository;
using ProviderMs.Domain.Entities;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Infrastructure.Repositories
{
    public class DepartamentRepository : IDepartamentRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public DepartamentRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task AddAsync(Departament departament)
        {
            await _dbContext.Departaments.AddAsync(departament);
            await _dbContext.SaveEfContextChanges("");
        }

        public async Task<Departament?> GetByIdAsync(DepartamentId id)
        {
            var departament = await _dbContext.Departaments.FirstOrDefaultAsync(x => x.Id == id);
            //TODO: Borrar todos los console
            return departament;
        }
        public async Task<List<Departament>?> GetAllAsync()
        {
            var departaments = await _dbContext.Departaments.ToListAsync();
            return departaments;
        }

        public async Task DeleteAsync(DepartamentId id)
        {
            var departament = await _dbContext.Departaments.FirstOrDefaultAsync(x => x.Id == id);
            if (departament == null) throw new DepartamentNotFoundException("departament not found");

            departament.IsDeleted = true;
            await _dbContext.SaveEfContextChanges("");
        }

        public async Task<Departament?> UpdateAsync(Departament departament)
        {
            _dbContext.Departaments.Update(departament);
            await _dbContext.SaveEfContextChanges("");
            return departament;
        }
         public Task<bool> ExistsAsync(DepartamentId id)
        {
            return _dbContext.Departaments.AnyAsync(x => x.Id == id);
        }
    }
}