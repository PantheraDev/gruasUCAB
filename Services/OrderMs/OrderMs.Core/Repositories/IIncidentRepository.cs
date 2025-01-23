using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderMs.Domain.Entities;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Core.Repositories
{
    public interface IIncidentRepository
    {
        Task<Incident?> GetByIdAsync(IncidentId id);
        Task AddAsync(Incident incident);
        Task DeleteAsync(IncidentId id);
        Task<List<Incident>?> GetAllAsync();
        Task<Incident?> UpdateAsync(Incident incident);
        Task<bool> ExistsAsync(IncidentId id);
    }
}