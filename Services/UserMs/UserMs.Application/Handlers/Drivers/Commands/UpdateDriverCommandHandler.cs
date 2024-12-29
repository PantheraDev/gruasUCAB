using UserMs.Application.Commands.Drivers;
using UserMs.Core.Repositories;
using UserMs.Domain.Entities;
using MediatR;
using UserMs.Infrastructure.Exceptions;

namespace UserMs.Application.Handlers.Drives.Commands
{
    public class UpdateDriverCommandHandler : IRequestHandler<UpdateDriverCommand,Driver>
    {
        private readonly IDriverRepository _driverRepository;

        public UpdateDriverCommandHandler(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public async Task<Driver> Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingDriver = await _driverRepository.GetDriverById(request.UserId);

                if (existingDriver == null)
                    throw new UserNotFoundException("Driver not found.");
                
                if (existingDriver.UserDelete.Value)
                    throw new UserNotFoundException("Driver not found.");

                if ((int)request.Driver.DriverAvailable < 0 || (int)request.Driver.DriverAvailable > 1)
                {
                    throw new NullAtributeException("DriverAvailable must be between 0 and 1");
                }

                existingDriver.SetUserEmail(UserEmail.Create(request.Driver.UserEmail.Value));
                existingDriver.SetUserPassword(UserPassword.Create(request.Driver.UserPassword.Value));
                existingDriver.SetUserProvider(UserProvider.Create(request.Driver.UserProvider.Value));
                existingDriver.SetUserDepartament(UserDepartament.Create(request.Driver.UserDepartament.Value));
                existingDriver.SetDriverAvailable(request.Driver.DriverAvailable);
                existingDriver.SetDriverLicenseId(LicenseId.Create(request.Driver.DriverLicenseId.Value));

                await _driverRepository.UpdateDriverAsync(request.UserId,existingDriver);

                return existingDriver;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}