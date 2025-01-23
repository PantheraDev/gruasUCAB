using Microsoft.EntityFrameworkCore;
using ProviderMs.Common.Exceptions;
using ProviderMs.Core.Database;
using ProviderMs.Core.Repository;
using ProviderMs.Domain.Entities;
using ProviderMs.Domain.ValueObjects;

namespace ProviderMs.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public DepartmentRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task AddAsync(Department department)
        {
            await _dbContext.Departments.AddAsync(department);
            await _dbContext.SaveEfContextChanges("");
        }

        public async Task<Department?> GetByIdAsync(DepartmentId id)
        {
            var department = await _dbContext.Departments.FirstOrDefaultAsync(x => x.Id == id);
            //TODO: Borrar todos los console
            return department;
        }
        public async Task<List<Department>?> GetAllAsync()
        {
            var departments = await _dbContext.Departments.ToListAsync();
            return departments;
        }

        public async Task DeleteAsync(DepartmentId id)
        {
            var department = await _dbContext.Departments.FirstOrDefaultAsync(x => x.Id == id);
            if (department == null) throw new DepartmentNotFoundException("department not found");

            department.IsDeleted = true;
            await _dbContext.SaveEfContextChanges("");
        }

        public async Task<Department?> UpdateAsync(Department department)
        {
            _dbContext.Departments.Update(department);
            await _dbContext.SaveEfContextChanges("");
            return department;
        }
        public Task<bool> ExistsAsync(DepartmentId id)
        {
            return _dbContext.Departments.AnyAsync(x => x.Id == id);
        }
    }
}