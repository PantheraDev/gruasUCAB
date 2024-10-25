using UserMs.Application.Commands.License;
using UserMs.Core.Repositories;
using UserMs.Domain.Entities;
using MediatR;
using UserMs.Infrastructure.Database;

namespace UserMs.Application.Handlers.License.Commands
{
    public class DeleteLicenseCommandHandler : IRequestHandler<DeleteLicenseCommand, LicenseId>
    {
        private readonly ILicenseRepository _licenseRepository;

        public DeleteLicenseCommandHandler(ILicenseRepository licenseRepository)
        {
            _licenseRepository = licenseRepository;
        }

        public async Task<LicenseId> Handle(DeleteLicenseCommand request, CancellationToken cancellationToken)
        {
            try{
                var license = await _licenseRepository.GetLicenseById(request.LicenseId);
                license.SetLicenseDelete(LicenseDelete.Create(true));
                await _licenseRepository.DeleteLicenseAsync(request.LicenseId);
                return request.LicenseId;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}