namespace UserMs.Domain.Entities
{
    public class Licensed
    {
        public LicenseId LicenseId { get; private set; }
        public LicenseDateExpiration LicenseDateExpiration { get; private set; }
        public LicenseNumber LicenseNumber { get; private set; }
        public LicenseDelete? LicenseDelete { get; private set; }
        public Licensed(LicenseId licenseId, LicenseDateExpiration licenseDateExpiration, LicenseNumber licenseNumber)
        {
            LicenseId = licenseId;
            LicenseDateExpiration = licenseDateExpiration;
            LicenseNumber = licenseNumber;
        }
        public void SetLicenseDateExpiration(LicenseDateExpiration licenseDateExpiration)
        {
            LicenseDateExpiration = licenseDateExpiration;
        }
        public void SetLicenseNumber(LicenseNumber licenseNumber)
        {
            LicenseNumber = licenseNumber;
        }
        public void SetLicenseDelete(LicenseDelete licenseDelete)
        {
            LicenseDelete = licenseDelete;
        }
    }
}