using ProviderMs.Domain.Entities;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Core.Repository
{
    public interface IProviderDepartmentRepository
    {
        Task<ProviderDepartment?> GetByIdAsync(ProviderDepartmentId id);
        Task<List<ProviderDepartment?>> GetAllAsync();
        Task<List<ProviderDepartment?>> GetByProviderAsync(ProviderId providerId);
        Task AddAsync(ProviderDepartment providerDepartment);
        Task<ProviderDepartment> UpdateAsync(ProviderDepartment providerDepartment, ProviderDepartment newProviderDepartment);
        Task DeleteAsync(ProviderDepartmentId id);
        Task<bool> RelationExistAsync(ProviderId providerId, DepartmentId departmentId);
    }
}