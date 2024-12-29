using UserMs.Application.Queries.Drivers;
using UserMs.Application.Dtos.Drivers.Response;
using UserMs.Core.Repositories;
using UserMs.Infrastructure.Exceptions;
using MediatR;

namespace UserMs.Application.Handlers.Drives.Queries
{
    public class GetDriverByIdQueryHandler : IRequestHandler<GetDriverByIdQuery, GetDriverDto>
    {
        private readonly IDriverRepository _driverRepository;

        public GetDriverByIdQueryHandler(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public async Task<GetDriverDto> Handle(GetDriverByIdQuery request, CancellationToken cancellationToken)
        {
            var driver = await _driverRepository.GetDriverById(request.Id);

            if (driver == null)
                throw new UserNotFoundException("Driver not found.");
            
            if (driver.UserDelete.Value)
                throw new UserNotFoundException("Driver not found.");

            return new GetDriverDto
            {
                UserId = driver.UserId.Value,
                UserEmail = driver.UserEmail.Value,
                UserPassword = driver.UserPassword.Value,
                DriverAvailable = driver.GetDriverAvailableString(),
                DriverLicenseId = driver.DriverLicenseId.Value,
                UserProvider = driver.UserProvider.Value,
                UserDepartament = driver.UserDepartament.Value
            };
        }
    }
}