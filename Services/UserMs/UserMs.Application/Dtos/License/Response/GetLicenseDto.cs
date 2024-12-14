namespace UserMs.Application.Dtos.License.Response{
    public class GetLicenseDto
    {
        public Guid LicenseId { get; set; }
        public DateTime LicenseDateExpiration { get; set; }
        public string? LicenseNumber { get; set; }
    }
}