using MediatR;
using UserMs.Domain.Entities;

namespace UserMs.Application.Commands.License
{
    public class DeleteLicenseCommand : IRequest<LicenseId>
    {
        public LicenseId LicenseId { get; set; }
        public DeleteLicenseCommand(LicenseId licenseId)
        {
            LicenseId = licenseId;
        }
    }
}