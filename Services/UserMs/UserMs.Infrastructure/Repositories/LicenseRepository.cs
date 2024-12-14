using UserMs.Core.Repositories;
using UserMs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using UserMs.Core.Database;

namespace UserMs.Infrastructure.Repositories
{
    public class LicenseRepository : ILicenseRepository
    {
        private readonly IUserDbContext _dbContext;

        public LicenseRepository(IUserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Licensed>> GetLicenseAsync()
        {
            var licenses = new List<Licensed>();
            await foreach (var license in _dbContext.License)
            {
                licenses.Add(license);
            }
            return licenses;
        }
        
        public async Task<Licensed?> GetLicenseById(LicenseId licenseId)
        {
            return await _dbContext.License.FindAsync(licenseId);
        }

        public async Task AddAsync(Licensed license)
        {
            await _dbContext.License.AddAsync(license);
            await _dbContext.SaveEfContextChanges("");
        }

        public async Task<Licensed?> UpdateLicenseAsync(LicenseId licenseId, Licensed license)
        {
            var existingLicense = await _dbContext.License.FindAsync(licenseId);
            await _dbContext.SaveEfContextChanges("");
            return existingLicense;
        }

        public async Task<Licensed?> DeleteLicenseAsync(LicenseId licenseId)
        {
            var existingLicense = await _dbContext.License.FindAsync(licenseId);
            await _dbContext.SaveEfContextChanges("");
            return existingLicense;
        }
    }
}