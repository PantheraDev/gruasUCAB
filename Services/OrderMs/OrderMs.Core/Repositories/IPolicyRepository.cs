using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderMs.Domain.Entities;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Core.Repositories
{
    public interface IPolicyRepository
    {
        Task<Policy?> GetByIdAsync(PolicyId id);
        Task AddAsync(Policy policy);
        Task DeleteAsync(PolicyId id);
        Task<List<Policy>?> GetAllAsync();
        Task<Policy?> UpdateAsync(Policy policy);
        Task<bool> ExistsAsync(PolicyId id);
    }
}