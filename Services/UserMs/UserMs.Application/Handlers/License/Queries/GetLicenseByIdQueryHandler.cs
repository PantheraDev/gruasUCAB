using UserMs.Application.Queries.License;
using UserMs.Application.Dtos.License.Response;
using UserMs.Core.Repositories;
using UserMs.Infrastructure.Exceptions;
using MediatR;

namespace UserMs.Application.Handlers.License.Queries
{
    public class GetLicenseByIdQueryHandler : IRequestHandler<GetLicenseByIdQuery, GetLicenseDto>
    {
        private readonly ILicenseRepository _licenseRepository;

        public GetLicenseByIdQueryHandler(ILicenseRepository licenseRepository)
        {
            _licenseRepository = licenseRepository;
        }

        public async Task<GetLicenseDto> Handle(GetLicenseByIdQuery request, CancellationToken cancellationToken)
        {
            var license = await _licenseRepository.GetLicenseById(request.Id);

            if (license == null)
                throw new UserNotFoundException("License not found.");
            
            if (license.LicenseDelete.Value)
                throw new UserNotFoundException("License not found.");

            return new GetLicenseDto
            {
                LicenseId = license.LicenseId.Value,
                LicenseDateExpiration = license.LicenseDateExpiration.Value,
                LicenseNumber = license.LicenseNumber.Value,
            };
        }
    }
}