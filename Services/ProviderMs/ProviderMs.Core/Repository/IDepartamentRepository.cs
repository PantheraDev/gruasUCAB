using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProviderMs.Domain.ValueObjects;
using ProviderMs.Domain.Entities;

namespace ProviderMs.Core.Repository
{
    public interface IDepartamentRepository
    {
        Task<Departament?> GetByIdAsync(DepartamentId id);
        Task AddAsync(Departament departament); 
        Task DeleteAsync(DepartamentId id);
        Task<List<Departament>?> GetAllAsync();
        Task<Departament?> UpdateAsync(Departament departament);
        Task<bool> ExistsAsync(DepartamentId id);
    }
}