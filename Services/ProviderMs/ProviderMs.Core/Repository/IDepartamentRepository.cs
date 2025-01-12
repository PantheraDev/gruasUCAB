using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProviderMs.Domain.ValueObjects;
using ProviderMs.Domain.Entities;

namespace ProviderMs.Core.Repository
{
    public interface IDepartmentRepository
    {
        Task<Department?> GetByIdAsync(DepartmentId id);
        Task AddAsync(Department department);
        Task DeleteAsync(DepartmentId id);
        Task<List<Department>?> GetAllAsync();
        Task<Department?> UpdateAsync(Department department);
        Task<bool> ExistsAsync(DepartmentId id);
    }
}