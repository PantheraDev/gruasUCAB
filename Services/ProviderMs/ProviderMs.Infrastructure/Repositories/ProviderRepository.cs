using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProviderMs.Core.Database;
using ProviderMs.Core.Repository;
using ProviderMs.Domain.Entities;
using ProviderMs.Domain.ValueObjects;
using ProviderMs.Common.Exceptions;

namespace ProviderMs.Infrastructure.Repositories
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public ProviderRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddAsync(Provider provider)
        {
            await _dbContext.Providers.AddAsync(provider);
            await _dbContext.SaveEfContextChanges("");
        }

        public async Task<Provider?> GetByIdAsync(ProviderId id /*Expression<Func<Provider, object>> include*/)
        {
            var provider = await _dbContext.Providers.FirstOrDefaultAsync(x => x.Id == id);
            //TODO: Borrar todos los console
            return provider;
            
        }
        public async Task<List<Provider>> GetAllAsync()
        {
            var providers = await _dbContext.Providers.ToListAsync();
            return providers ?? new List<Provider>();
            
            
        }

        public async Task DeleteAsync(ProviderId id)
        {
            var providerDepartaments = await _dbContext.ProviderDepartaments
                .Where(pd => pd.ProviderId == id) // Solo obtener los no eliminados
                .ToListAsync();

            foreach (var providerDepartament in providerDepartaments)
            {
                providerDepartament.IsDeleted = true;
            }

            var provider = await _dbContext.Providers.FindAsync(id); // Más eficiente que FirstOrDefaultAsync por Id
            if (provider != null) provider.IsDeleted = true; // Solo si se encuentra el proveedor

            await _dbContext.SaveEfContextChanges("");
        }

        public async Task<Provider?> UpdateAsync(Provider provider)
        {
            _dbContext.Providers.Update(provider);
            await _dbContext.SaveEfContextChanges("");
            return provider;
        }
    }
}