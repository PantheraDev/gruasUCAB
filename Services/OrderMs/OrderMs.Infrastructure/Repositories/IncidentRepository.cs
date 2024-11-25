//TODO: Borrar todos los console, los using innecesarios y los comentarios innecesarios
using Microsoft.EntityFrameworkCore;
using OrderMs.Common.Exceptions;
using OrderMs.Core.Database;
using OrderMs.Core.Repositories;
using OrderMs.Domain.Entities;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Infrastructure.Repositories
{
    public class IncidentRepository : IIncidentRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public IncidentRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddAsync(Incident incident)
        {
            await _dbContext.Incidents.AddAsync(incident);
            await _dbContext.SaveEfContextChanges("");
        }

        public async Task<Incident?> GetByIdAsync(IncidentId id)
        {
            var incident = await _dbContext.Incidents.FirstOrDefaultAsync(x => x.Id == id);
            //TODO: Borrar todos los console
            Console.WriteLine("This is the Incident:" + incident);
            return incident;
        }
        public async Task<List<Incident>?> GetAllAsync()
        {
            var incidents = await _dbContext.Incidents.ToListAsync();
            return incidents;
        }

        public async Task DeleteAsync(IncidentId id)
        {
            var incident = await _dbContext.Incidents.FirstOrDefaultAsync(x => x.Id == id);
            if (incident == null) throw new IncidentNotFoundException("Incident not found");

            incident.IsDeleted = true;
            await _dbContext.SaveEfContextChanges("");
        }

        public async Task<Incident?> UpdateAsync(Incident incident)
        {
            _dbContext.Incidents.Update(incident);
            await _dbContext.SaveEfContextChanges("");
            return incident;
        }

        public Task<bool> ExistsAsync(IncidentId id)
        {
            return _dbContext.Incidents.AnyAsync(x => x.Id == id);
        }
    }
}