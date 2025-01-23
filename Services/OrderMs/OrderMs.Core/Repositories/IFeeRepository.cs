using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderMs.Domain.Entities;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Core.Repositories
{
    public interface IFeeRepository
    {
        Task<Fee?> GetByIdAsync(FeeId id);
        Task AddAsync(Fee fee);
        Task DeleteAsync(FeeId id);
        Task<List<Fee>?> GetAllAsync();
        Task<Fee?> UpdateAsync(Fee fee);
        Task<bool> ExistsAsync(FeeId id);
    }
}