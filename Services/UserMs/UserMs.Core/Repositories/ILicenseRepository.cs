using UserMs.Domain.Entities;

namespace UserMs.Core.Repositories{
    public interface ILicenseRepository {
        Task<List<Licensed>> GetLicenseAsync();
        Task<Licensed?> GetLicenseById(LicenseId licenseId);
        Task AddAsync(Licensed license);
        Task<Licensed?> UpdateLicenseAsync(LicenseId licenseId, Licensed license);
        Task<Licensed?> DeleteLicenseAsync(LicenseId licenseId);
    }
}