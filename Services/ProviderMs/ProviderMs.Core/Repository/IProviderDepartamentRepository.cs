using ProviderMs.Domain.Entities;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Core.Repository
{
    public interface IProviderDepartamentRepository
    {
        Task<List<ProviderDepartament?>> GetByProviderIdAsync(ProviderId id); 
        Task<List<ProviderDepartament?>> GetAllAsync();
        Task<ProviderDepartament?> UpdateAsync(ProviderDepartament providerDepartament);
        Task DeleteAsync(ProviderId providerId, DepartamentId departamentId);
    }
}