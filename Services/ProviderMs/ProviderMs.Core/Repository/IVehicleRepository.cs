using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProviderMs.Domain.ValueObjects;
using ProviderMs.Domain.Entities;

namespace ProviderMs.Core.Repository
{
    public interface ITowRepository
    {
        Task<Tow?> GetByIdAsync(VehicleId id);
        Task AddAsync(Tow tow); 
        Task DeleteAsync(VehicleId id);
        Task<List<Tow>?> GetAllAsync();
        Task<Tow?> UpdateAsync(Tow tow);

        Task<bool> ExistsAsync(VehicleId id);
    }
}