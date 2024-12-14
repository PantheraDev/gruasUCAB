namespace UserMs.Application.Dtos.Drivers.Response{
    public class GetDriverDto
    {
        public Guid UserId { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPassword { get; set; }
        public string? DriverAvailable { get; set; }
        public Guid? DriverLicenseId { get; set; }
    }
}