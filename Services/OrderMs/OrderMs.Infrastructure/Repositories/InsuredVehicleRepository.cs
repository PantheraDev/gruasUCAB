//TODO: Borrar todos los console, los using innecesarios y los comentarios innecesarios
using Microsoft.EntityFrameworkCore;
using OrderMs.Common.Exceptions;
using OrderMs.Core.Database;
using OrderMs.Core.Repositories;
using OrderMs.Domain.Entities;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Infrastructure.Repositories
{
    public class InsuredVehicleRepository : IInsuredVehicleRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public InsuredVehicleRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddAsync(InsuredVehicle insuredVehicle)
        {
            await _dbContext.InsuredVehicles.AddAsync(insuredVehicle);
            await _dbContext.SaveEfContextChanges("");
        }

        public async Task<InsuredVehicle?> GetByIdAsync(InsuredVehicleId id)
        {
            var insuredVehicle = await _dbContext.InsuredVehicles.FirstOrDefaultAsync(x => x.Id == id);
            //TODO: Borrar todos los console
            Console.WriteLine("This is the InsuredVehicle:" + insuredVehicle);
            return insuredVehicle;
        }
        public async Task<List<InsuredVehicle>?> GetAllAsync()
        {
            var insuredVehicles = await _dbContext.InsuredVehicles.ToListAsync();
            return insuredVehicles;
        }

        public async Task DeleteAsync(InsuredVehicleId id)
        {
            var insuredVehicle = await _dbContext.InsuredVehicles.FirstOrDefaultAsync(x => x.Id == id);
            if (insuredVehicle == null) throw new InsuredVehicleNotFoundException("InsuredVehicle not found");

            insuredVehicle.IsDeleted = true;
            await _dbContext.SaveEfContextChanges("");
        }

        public async Task<InsuredVehicle?> UpdateAsync(InsuredVehicle insuredVehicle)
        {
            _dbContext.InsuredVehicles.Update(insuredVehicle);
            await _dbContext.SaveEfContextChanges("");
            return insuredVehicle;
        }

        public Task<bool> ExistsAsync(InsuredVehicleId id)
        {
            return _dbContext.InsuredVehicles.AnyAsync(x => x.Id == id);
        }
    }
}