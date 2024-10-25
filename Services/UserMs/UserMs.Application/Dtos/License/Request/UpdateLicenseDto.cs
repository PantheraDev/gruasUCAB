using UserMs.Domain.Entities;

namespace UserMs.Application.Dtos.License.Request{
    public class UpdateLicenseDto
    {
        public LicenseDateExpiration? LicenseDateExpiration { get; set; }
        public LicenseNumber? LicenseNumber { get; set; }
    }
}