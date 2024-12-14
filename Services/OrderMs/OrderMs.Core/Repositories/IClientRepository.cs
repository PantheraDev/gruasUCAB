using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderMs.Domain.Entities;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Core.Repositories
{
    public interface IClientRepository
    {
        Task<Client?> GetByIdAsync(ClientId id);
        Task AddAsync(Client client);
        Task DeleteAsync(ClientId id);
        Task<List<Client>?> GetAllAsync();
        Task<Client?> UpdateAsync(Client client);
        Task<bool> ExistAsync(ClientId id);
    }
}