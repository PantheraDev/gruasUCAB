namespace UserMs.Domain.Entities
{
    public class Driver : Base
    {
        public DriverAvailable DriverAvailable { get; private set; }
        public Licensed DriverLicense { get; private set; }
        public LicenseId DriverLicenseId { get; private set; }

        public Driver(UserId userId, UserEmail userEmail, UserPassword userPassword, UserDepartament userDepartament, DriverAvailable driverAvailable, LicenseId driverLicenseId)
            : base(userId, userEmail, userPassword, userDepartament)
        {
            DriverAvailable = driverAvailable;
            DriverLicenseId = driverLicenseId;
        }

        public string GetDriverAvailableString()
        {
            return DriverAvailable.ToString();
        }

        public void SetDriverAvailable(DriverAvailable driverAvailable)
        {
            DriverAvailable = driverAvailable;
        }

        public void SetDriverLicense(Licensed license)
        {
            DriverLicense = license;
        }

        public void SetDriverLicenseId(LicenseId driverLicenseId)
        {
            DriverLicenseId = driverLicenseId;
        }
    }
}