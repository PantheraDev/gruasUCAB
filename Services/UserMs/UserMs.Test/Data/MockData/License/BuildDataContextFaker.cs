using UserMs.Application.Dtos.License.Request;
using UserMs.Application.Dtos.License.Response;
using UserMs.Domain.Entities;
using UserMs.Application.Commands.License;

namespace UserMs.Test.Data.MockData.License
{
    public static class BuildDataContextFaker
    {
        public static CreateLicenseCommand BuildCreateLicenseCommand()
        {
            var license = new CreateLicenseDto()
            {
                LicenseId = LicenseId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822")), 
                LicenseDateExpiration = LicenseDateExpiration.Create(new DateTime(2026-09-10)),
                LicenseNumber = LicenseNumber.Create("109031444")
            };
            return new CreateLicenseCommand(license);
        }

        public static DeleteLicenseCommand BuildDeleteLicenseCommand()
        {
            var licenseId = LicenseId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822"));
            return new DeleteLicenseCommand(licenseId);
        }

        public static UpdateLicenseCommand BuildUpdateLicenseCommand()
        {
            var licenseId = LicenseId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822"));
            var license = new UpdateLicenseDto()
            {
                LicenseDateExpiration = LicenseDateExpiration.Create(new DateTime(2026-09-10)),
                LicenseNumber = LicenseNumber.Create("109031444"),
            };
            return new UpdateLicenseCommand(licenseId,license);
        }

        public static Licensed BuildCreateLicenseEntity()
        {
            var license = new Licensed(
                LicenseId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822")), 
                LicenseDateExpiration.Create(new DateTime(2026-09-10)), 
                LicenseNumber.Create("109031444")
                );
                
            return license;
        }

        public static List<Licensed> BuildCreateLicenseEntityList()
        {
            var license1 = new Licensed(
                LicenseId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822")), 
                LicenseDateExpiration.Create(new DateTime(2026-09-10)), 
                LicenseNumber.Create("109031444")
            );

            var license2 = new Licensed(
                LicenseId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209820")), 
                LicenseDateExpiration.Create(new DateTime(2025-05-20)), 
                LicenseNumber.Create("12312412")
            );

            return new List<Licensed> { license1, license2 };
        }

        public static Licensed BuildUpdateLicenseEntity()
        {
            var license = new Licensed(
                LicenseId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822")),
                LicenseDateExpiration.Create(new DateTime(2030-11-28)),
                LicenseNumber.Create("109031590")
            );

            return license;
        }

        public static CreateLicenseDto GenerateCreateLicenseDto()
        {
            return new CreateLicenseDto()
            {
                LicenseId = LicenseId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822")),
                LicenseDateExpiration = LicenseDateExpiration.Create(new DateTime(2026-11-02)),
                LicenseNumber = LicenseNumber.Create("124124213"),
            };
        }

        public static Task<List<GetLicenseDto>> GenerateGetLicenseDtoList()
        {
            var licenseDto = new GetLicenseDto()
            {
                LicenseId = new Guid("c0869fe3-0236-4542-9b46-18fb2e209822"),
                LicenseDateExpiration = new DateTime(2026-11-02),
                LicenseNumber = "1213123124",
            };

            return Task.FromResult(new List<GetLicenseDto>() { licenseDto });
        }

        public static Task<GetLicenseDto> GenerateGetLicenseDto()
        {
            var licenseDto = new GetLicenseDto()
            {
                LicenseId = new Guid("c0869fe3-0236-4542-9b46-18fb2e209822"),
                LicenseDateExpiration = new DateTime(2026-11-02),
                LicenseNumber = "1213123124",
            };

            return Task.FromResult(licenseDto);
        }

        public static UpdateLicenseDto GenerateUpdateLicenseDto()
        {
            return new UpdateLicenseDto()
            {
                LicenseDateExpiration = LicenseDateExpiration.Create(new DateTime(2026-11-02)),
                LicenseNumber = LicenseNumber.Create("124124213"),
            };
        }

        public static Task<Licensed> GenerateLicensed()
        {
            var licensed = new Licensed(
                LicenseId.Create(new Guid("c0869fe3-0236-4542-9b46-18fb2e209822")),
                LicenseDateExpiration.Create(new DateTime(2026-11-02)),
                LicenseNumber.Create("124124213")
            );

            return Task.FromResult(licensed);
        }

        public static Task<LicenseId> GetLicenseId()
        {
            var licenseId = LicenseId.Create(Guid.NewGuid());

            return Task.FromResult(licenseId);
        }
    }
}