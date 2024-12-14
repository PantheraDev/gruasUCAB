using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderMs.Domain.Entities;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Core.Repositories
{
    public interface IAdditionalCostRepository
    {
        Task<AdditionalCost?> GetByIdAsync(AdditionalCostId id);
        Task AddAsync(AdditionalCost additionalCost);
        Task DeleteAsync(AdditionalCostId id);
        Task<List<AdditionalCost>?> GetAllAsync();
        Task<AdditionalCost?> UpdateAsync(AdditionalCost additionalCost);
        Task<bool> ExistsAsync(AdditionalCostId id);
    }
}