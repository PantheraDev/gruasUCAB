using UserMs.Application.Dtos.License.Request;
using MediatR;
using UserMs.Domain.Entities;

namespace UserMs.Application.Commands.License
{
    public class UpdateLicenseCommand : IRequest<Licensed>
    {
        public LicenseId LicenseId { get; set; }
        public UpdateLicenseDto License { get; set; }

        public UpdateLicenseCommand(LicenseId licenseId, UpdateLicenseDto license)
        {
            LicenseId = licenseId;
            License = license;
        }
    }
}