using UserMs.Application.Commands.License;
using UserMs.Application.Validators;
using UserMs.Core.Repositories;
using UserMs.Domain.Entities;
using MediatR;
using UserMs.Infrastructure.Exceptions;

namespace UserMs.Application.Handlers.License.Commands
{
    public class CreateLicenseCommandHandler : IRequestHandler<CreateLicenseCommand, LicenseId>
    {
        private readonly ILicenseRepository _licenseRepository;

        public CreateLicenseCommandHandler(ILicenseRepository licenseRepository)
        {
            _licenseRepository = licenseRepository;
        }

        public async Task<LicenseId> Handle(CreateLicenseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new CreateLicenseValidator();
                await validator.ValidateRequest(request.License);

                var licenseDateExpirationValue = request.License.LicenseDateExpiration?.Value;
                var licenseNumberValue = request.License.LicenseNumber?.Value;

                var license = new Licensed(
                    LicenseId.Create(),
                    LicenseDateExpiration.Create(licenseDateExpirationValue ?? DateTime.Now),
                    LicenseNumber.Create(licenseNumberValue ?? string.Empty)
                );

                await _licenseRepository.AddAsync(license);

                return license.LicenseId;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}