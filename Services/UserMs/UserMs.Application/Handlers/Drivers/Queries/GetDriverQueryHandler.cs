using UserMs.Application.Queries.Drivers;
using UserMs.Application.Dtos.Drivers.Response;
using UserMs.Core.Repositories;
using MediatR;
using UserMs.Infrastructure.Exceptions;

namespace UserMs.Application.Handlers.Drives.Queries
{
    public class GetDriverQueryHandler : IRequestHandler<GetDriverQuery, List<GetDriverDto>>
    {
        private readonly IDriverRepository _driverRepository;
        //!: Hay un problema de dependencias, Esta capa no puede utilizar infraestructura
        public GetDriverQueryHandler(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public async Task<List<GetDriverDto>> Handle(GetDriverQuery request, CancellationToken cancellationToken)
        {
            var driver = await _driverRepository.GetDriverAsync();
            var activeDrivers = driver.Where(u => !u.UserDelete.Value).ToList();

            return activeDrivers.Select(driver => new GetDriverDto
            {
                UserId = driver.UserId.Value,
                UserEmail = driver.UserEmail.Value,
                UserPassword = driver.UserPassword.Value,
                DriverAvailable = driver.GetDriverAvailableString(),
                DriverLicenseId = driver.DriverLicenseId.Value,
                UserDepartament = driver.UserDepartament.Value
            }).ToList();
        }
    }
}