using UserMs.Application.Dtos.Drivers.Request;
using FluentValidation;

namespace UserMs.Application.Validators
{
    public class CreateDriverValidator : ValidatorBase<CreateDriverDto>
    {
        public CreateDriverValidator()
        {
            RuleFor(s => s.UserEmail).NotNull().WithMessage("UserEmail no puede ser nulo").WithErrorCode("600");
            RuleFor(s => s.DriverAvailable).NotNull().WithMessage("DriverAvailable no puede ser nulo").WithErrorCode("600");
            RuleFor(s => s.DriverLicenseId).NotNull().WithMessage("DriverLicenseId no puede ser nulo").WithErrorCode("600");
        }
    }
}