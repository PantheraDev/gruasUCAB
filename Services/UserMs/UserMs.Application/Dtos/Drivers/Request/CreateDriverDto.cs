using UserMs.Domain.Entities;

namespace UserMs.Application.Dtos.Drivers.Request{
    public class CreateDriverDto
    {
        public UserEmail? UserEmail { get; set; }
        public UserPassword? UserPassword { get; set; }
        public string? DriverAvailable { get; init; }
        public LicenseId? DriverLicenseId { get; set; }
        public UserProvider? UserProvider { get; set; }
        public UserDepartament? UserDepartament { get; set; }
    }
}