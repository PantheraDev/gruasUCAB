using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderMs.Domain.Entities;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Core.Repositories
{
    public interface IInsuredVehicleRepository
    {
        Task<InsuredVehicle?> GetByIdAsync(InsuredVehicleId id);
        Task AddAsync(InsuredVehicle insuredVehicle);
        Task DeleteAsync(InsuredVehicleId id);
        Task<List<InsuredVehicle>?> GetAllAsync();
        Task<InsuredVehicle?> UpdateAsync(InsuredVehicle insuredVehicle);
        Task<bool> ExistsAsync(InsuredVehicleId id);
    }
}