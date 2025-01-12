using UserMs.Application.Commands.Drivers;
using UserMs.Application.Validators;
using UserMs.Core.Repositories;
using UserMs.Domain.Entities;
using MediatR;
using UserMs.Infrastructure.Exceptions;
using UserMs.Core.Interface;

namespace UserMs.Application.Handlers.Drives.Commands
{
    public class CreateDriverCommandHandler : IRequestHandler<CreateDriverCommand, UserId>
    {
        private readonly IDriverRepository _driverRepository;
        private readonly ILicenseRepository _licenseRepository;
        private readonly IAuthMsService _authMsService;

        public CreateDriverCommandHandler(IDriverRepository driverRepository, ILicenseRepository licenseRepository, IAuthMsService authMsService)
        {
            _driverRepository = driverRepository;
            _licenseRepository = licenseRepository;
            _authMsService = authMsService;
        }

        public async Task<UserId> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new CreateDriverValidator();
                await validator.ValidateRequest(request.Driver);

                Func<string> generatePassword = () =>
                                {
                                    const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
                                    var random = new Random();
                                    return new string(Enumerable.Repeat(validChars, 8)
                                        .Select(s => s[random.Next(s.Length)]).ToArray());
                                };

                var userEmailValue = request.Driver.UserEmail;
                var userPasswordValue = generatePassword();
                var driverLicenseId = request.Driver.DriverLicenseId;
                var userDepartamentValue = request.Driver.UserDepartament;

                await _authMsService.CreateUserAsync(userEmailValue!, userPasswordValue);
                var userId = await _authMsService.GetUserByUserName(UserEmail.Create(userEmailValue!));
                await _authMsService.AssignClientRoleToUser(userId, "Conductor");

                var driver = new Driver(
                    UserId.Create(userId),
                    UserEmail.Create(userEmailValue ?? string.Empty),
                    UserPassword.Create(userPasswordValue ?? string.Empty),
                    UserDepartament.Create(userDepartamentValue),
                    Enum.Parse<DriverAvailable>(request.Driver.DriverAvailable!),
                    LicenseId.Create(driverLicenseId)
                );

                if (request.Driver.DriverAvailable == null)
                {
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