using UserMs.Domain.Entities;

namespace UserMs.Application.Dtos.License.Request{
    public class CreateLicenseDto
    {
        public LicenseDateExpiration? LicenseDateExpiration { get; set; }
        public LicenseNumber? LicenseNumber { get; set; }
    }
}