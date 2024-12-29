using Microsoft.EntityFrameworkCore;
using ProviderMs.Core.Database;
using ProviderMs.Core.Repository;
using ProviderMs.Domain.Entities;
using ProviderMs.Common.Exceptions;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Infrastructure.Repositories
{
    public class ProviderDepartamentRepository : IProviderDepartamentRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public ProviderDepartamentRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<List<ProviderDepartament?>> GetByProviderIdAsync(ProviderId id)
        {
            var providerDepartaments = await _dbContext.ProviderDepartaments.Where(x => x.ProviderId == id).ToListAsync();

            // Opcional: Eliminar o modificar el Console.WriteLine
            Console.WriteLine("Cantidad de ProviderDepartaments encontrados: " + providerDepartaments.Count);

            return providerDepartaments;
        }
        public async Task<List<ProviderDepartament?>> GetAllAsync()
        {
            var providerDepartaments = await _dbContext.ProviderDepartaments.ToListAsync();
            return providerDepartaments;
        }
        public async Task<ProviderDepartament?> UpdateAsync(ProviderDepartament providerDepartament)
        {
            _dbContext.ProviderDepartaments.Update(providerDepartament);
            await _dbContext.SaveEfContextChanges("");
            return providerDepartament;
        }
        public async Task DeleteAsync(ProviderId providerId, DepartamentId departamentId)
        {
            var providerDepartament = await _dbContext.ProviderDepartaments.FirstOrDefaultAsync(x => x.ProviderId == providerId && x.DepartamentId == departamentId);
            if (providerDepartament == null) throw new DepartamentNotFoundException("ProviderDepartament not found");

            providerDepartament.IsDeleted = true;
            await _dbContext.SaveEfContextChanges("");
        }
    }
}