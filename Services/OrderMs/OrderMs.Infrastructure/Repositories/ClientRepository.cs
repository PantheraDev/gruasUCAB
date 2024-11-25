using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderMs.Common.Exceptions;
using OrderMs.Core.Database;
using OrderMs.Core.Repositories;
using OrderMs.Domain.Entities;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public ClientRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddAsync(Client client)
        {
            await _dbContext.Clients.AddAsync(client);
            await _dbContext.SaveEfContextChanges("");
        }

        public async Task<Client?> GetByIdAsync(ClientId id)
        {
            var client = await _dbContext.Clients.FirstOrDefaultAsync(x => x.Id == id);
            //TODO: Borrar todos los console
            Console.WriteLine("This is the client:" + client);
            return client;
        }
        public async Task<List<Client>?> GetAllAsync()
        {
            var clients = await _dbContext.Clients.ToListAsync();
            return clients;
        }

        public async Task DeleteAsync(ClientId id)
        {
            var client = await _dbContext.Clients.FirstOrDefaultAsync(x => x.Id == id);
            if (client == null) throw new ClientNotFoundException("Client not found");

            client.IsDeleted = true;
            await _dbContext.SaveEfContextChanges("");
        }

        public async Task<Client?> UpdateAsync(Client client)
        {
            _dbContext.Clients.Update(client);
            await _dbContext.SaveEfContextChanges("");
            return client;
        }
        public Task<bool> ExistAsync(ClientId id)
        {
            return _dbContext.Clients.AnyAsync(x => x.Id == id);
        }
    }
}