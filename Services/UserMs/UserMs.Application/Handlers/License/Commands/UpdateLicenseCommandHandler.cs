using UserMs.Application.Commands.License;
using UserMs.Core.Repositories;
using UserMs.Domain.Entities;
using MediatR;
using UserMs.Infrastructure.Exceptions;

namespace UserMs.Application.Handlers.License.Commands
{
    public class UpdateLicenseCommandHandler : IRequestHandler<UpdateLicenseCommand,Licensed>
    {
        private readonly ILicenseRepository _licenseRepository;

        public UpdateLicenseCommandHandler(ILicenseRepository licenseRepository)
        {
            _licenseRepository = licenseRepository;
        }

        public async Task<Licensed> Handle(UpdateLicenseCommand request, CancellationToken cancellationToken)
        {
            try{
                var existingLicense = await _licenseRepository.GetLicenseById(request.LicenseId);

                if (existingLicense == null)
                    throw new UserNotFoundException("License not found.");
                
                if (existingLicense.LicenseDelete.Value)
                    throw new UserNotFoundException("License not found.");

                existingLicense.SetLicenseDateExpiration(LicenseDateExpiration.Create(request.License.LicenseDateExpiration.Value));
                existingLicense.SetLicenseNumber(LicenseNumber.Create(request.License.LicenseNumber.Value));

                await _licenseRepository.UpdateLicenseAsync(request.LicenseId,existingLicense);

                return existingLicense;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}