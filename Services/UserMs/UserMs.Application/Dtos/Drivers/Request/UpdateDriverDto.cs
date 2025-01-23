using UserMs.Domain.Entities;

namespace UserMs.Application.Dtos.Drivers.Request
{
    public class UpdateDriverDto
    {
        public string? UserEmail { get; set; }
        public string? UserPassword { get; set; }
        public DriverAvailable DriverAvailable { get; set; }
        public Guid? DriverLicenseId { get; set; }
        public Guid? UserDepartament { get; set; }
    }
}