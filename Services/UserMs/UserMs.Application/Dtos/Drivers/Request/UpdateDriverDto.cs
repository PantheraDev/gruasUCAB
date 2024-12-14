using UserMs.Domain.Entities;

namespace UserMs.Application.Dtos.Drivers.Request{
    public class UpdateDriverDto
    {
        public UserEmail? UserEmail { get; set; }
        public UserPassword? UserPassword { get; set; }
        public DriverAvailable DriverAvailable { get; set; }
        public LicenseId? DriverLicenseId { get; set; }
    }
}