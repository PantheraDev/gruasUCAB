using UserMs.Application.Commands.Drivers;
using UserMs.Application.Validators;
using UserMs.Core.Repositories;
using UserMs.Domain.Entities;
using MediatR;
using UserMs.Infrastructure.Exceptions;

namespace UserMs.Application.Handlers.Drives.Commands
{
    public class CreateDriverCommandHandler : IRequestHandler<CreateDriverCommand, UserId>
    {
        private readonly IDriverRepository _driverRepository;
        private readonly ILicenseRepository _licenseRepository;

        public CreateDriverCommandHandler(IDriverRepository driverRepository, ILicenseRepository licenseRepository)
        {
            _driverRepository = driverRepository;
            _licenseRepository = licenseRepository;
        }

        public async Task<UserId> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new CreateDriverValidator();
                await validator.ValidateRequest(request.Driver);

                var userEmailValue = request.Driver.UserEmail?.Value;
                var userPasswordValue = request.Driver.UserPassword?.Value;
                var driverLicenseId = request.Driver.DriverLicenseId;

                var driver = new Driver(
                    UserId.Create(),
                    UserEmail.Create(userEmailValue ?? string.Empty),
                    UserPassword.Create(userPasswordValue ?? string.Empty),
                    Enum.Parse<DriverAvailable>(request.Driver.DriverAvailable!),
                    driverLicenseId
                );

                if(request.Driver.DriverAvailable == null){
                throw new NullAtributeException("UsersType can't be null");
                }

                await _driverRepository.AddAsync(driver);

                return driver.UserId;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}