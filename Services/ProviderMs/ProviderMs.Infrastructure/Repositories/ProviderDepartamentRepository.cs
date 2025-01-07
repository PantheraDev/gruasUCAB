using Microsoft.EntityFrameworkCore;
using ProviderMs.Core.Database;
using ProviderMs.Core.Repository;
using ProviderMs.Domain.Entities;
using ProviderMs.Common.Exceptions;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Infrastructure.Repositories
{
    public class ProviderDepartmentRepository : IProviderDepartmentRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public ProviderDepartmentRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<ProviderDepartment?> GetByIdAsync(ProviderDepartmentId id /*Expression<Func<Provider, object>> include*/)
        {
            var providerDepartment = await _dbContext.ProviderDepartments.FirstOrDefaultAsync(x => x.Id == id);
            //TODO: Borrar todos los console
            return providerDepartment;
        }

        public async Task<List<ProviderDepartment?>> GetByProviderAsync(ProviderId providerId /*Expression<Func<Provider, object>> include*/)
        {
            var providerDepartment = await _dbContext.ProviderDepartments.Where(x => x.ProviderId == providerId).ToListAsync();
            //TODO: Borrar todos los console
            return providerDepartment!;
        }

        public async Task<List<ProviderDepartment?>> GetAllAsync()
        {
            var providerDepartments = await _dbContext.ProviderDepartments.ToListAsync();
            return providerDepartments!;
        }

        public async Task AddAsync(ProviderDepartment providerDepartment)
        {
            await _dbContext.ProviderDepartments.AddAsync(providerDepartment);
            await _dbContext.SaveEfContextChanges("");
        }

        public async Task<ProviderDepartment> UpdateAsync(ProviderDepartment oldProviderDepartmentm, ProviderDepartment newProviderDepartment)
        {
            _dbContext.ProviderDepartments.Remove(oldProviderDepartmentm);
            var newId = this.AddAsync(newProviderDepartment);
            return newProviderDepartment;
        }

        public async Task DeleteAsync(ProviderDepartmentId id)
        {
            var providerDepartment = await _dbContext.ProviderDepartments.FirstOrDefaultAsync(x => x.Id == id);
            if (providerDepartment == null) throw new DepartmentNotFoundException("ProviderDepartment not found");

            providerDepartment.IsDeleted = true;
            await _dbContext.SaveEfContextChanges("");
        }

        public async Task<bool> RelationExistAsync(ProviderId providerId, DepartmentId departmentId)
        {
            var providerDepartment = await _dbContext.ProviderDepartments.FirstOrDefaultAsync(x => x.ProviderId == providerId && x.DepartmentId == departmentId && x.IsDeleted == false);
            return providerDepartment != null;
        }

    }
}