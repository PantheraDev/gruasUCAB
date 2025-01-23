using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProviderMs.Domain.ValueObjects;
using ProviderMs.Domain.Entities;
using System.Linq.Expressions;

namespace ProviderMs.Core.Repository
{
    public interface IProviderRepository
    {
        Task<Provider?> GetByIdAsync(ProviderId id/*, Expression<Func<Provider, object>> include*/);
        Task AddAsync(Provider provider); 
        Task DeleteAsync(ProviderId id);
        Task<List<Provider>> GetAllAsync();
        Task<Provider?> UpdateAsync(Provider provider);
    }
}