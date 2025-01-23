using UserMs.Domain.Entities;

namespace UserMs.Application.Dtos.Drivers.Request{
    public class CreateDriverDto
    {
        public string UserEmail { get; set; } = String.Empty;
        public string? DriverAvailable { get; init; }
        public Guid DriverLicenseId { get; set; }
        public Guid UserDepartament { get; set; }
    }
}