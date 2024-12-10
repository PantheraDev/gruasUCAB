using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProviderMs.Core.Database;
using ProviderMs.Core.Repository;
using ProviderMs.Domain.Entities;
using ProviderMs.Domain.ValueObjects;
using ProviderMs.Common.Exceptions;

namespace ProviderMs.Infrastructure.Repositories
{
    public class TowRepository : ITowRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public TowRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddAsync(Tow tow)
        {
            await _dbContext.Tows.AddAsync(tow);
            await _dbContext.SaveEfContextChanges("");
        }

        public async Task<Tow?> GetByIdAsync(VehicleId id)
        {
            var tow = await _dbContext.Tows.FirstOrDefaultAsync(x => x.Id == id);
            //TODO: Borrar todos los console
            return tow;
        }
        public async Task<List<Tow>?> GetAllAsync()
        {
            var tows = await _dbContext.Tows.ToListAsync();
            return tows;
        }

        public async Task DeleteAsync(VehicleId id)
        {
            var tow = await _dbContext.Tows.FirstOrDefaultAsync(x => x.Id == id);
            if (tow == null) throw new VehicleNotFoundException("tow not found");

            tow.IsDeleted = true;
            await _dbContext.SaveEfContextChanges("");
        }

        public async Task<Tow?> UpdateAsync(Tow vehicle)
        {
            _dbContext.Tows.Update(vehicle);
            await _dbContext.SaveEfContextChanges("");
            return vehicle;
        }

        public Task<bool> ExistsAsync(VehicleId id)
        {
            return _dbContext.Tows.AnyAsync(x => x.Id == id);
        }
    }
}