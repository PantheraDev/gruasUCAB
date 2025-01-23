using UserMs.Application.Queries.License;
using UserMs.Application.Dtos.License.Response;
using UserMs.Core.Repositories;
using MediatR;
using UserMs.Infrastructure.Exceptions;

namespace UserMs.Application.Handlers.License.Queries
{
    public class GetLicenseQueryHandler : IRequestHandler<GetLicenseQuery, List<GetLicenseDto>>
    {
        private readonly ILicenseRepository _licenseRepository;
        //!: Hay un problema de dependencias, Esta capa no puede utilizar infraestructura
        public GetLicenseQueryHandler(ILicenseRepository licenseRepository)
        {
            _licenseRepository = licenseRepository;
        }

        public async Task<List<GetLicenseDto>> Handle(GetLicenseQuery request, CancellationToken cancellationToken)
        {
            var license = await _licenseRepository.GetLicenseAsync();
            var activeLicense = license.Where(u => !u.LicenseDelete.Value).ToList();

            return activeLicense.Select(license => new GetLicenseDto
            {
                LicenseId = license.LicenseId.Value,
                LicenseDateExpiration = license.LicenseDateExpiration.Value,
                LicenseNumber = license.LicenseNumber.Value
            }).ToList();
        }
    }
}