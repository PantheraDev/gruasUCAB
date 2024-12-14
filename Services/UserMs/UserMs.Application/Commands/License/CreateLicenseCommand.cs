using UserMs.Application.Dtos.License.Request;
using MediatR;
using UserMs.Domain.Entities;

namespace UserMs.Application.Commands.License
{
    public class CreateLicenseCommand : IRequest<LicenseId>
    {
        public CreateLicenseDto? License { get; set; }

        public CreateLicenseCommand(CreateLicenseDto? license)
        {
            License = license;
        }
    }
}