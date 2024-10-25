using UserMs.Application.Dtos.License.Request;
using FluentValidation;

namespace UserMs.Application.Validators
{
    public class CreateLicenseValidator : ValidatorBase<CreateLicenseDto>
    {
        public CreateLicenseValidator()
        {
            RuleFor(s => s.LicenseDateExpiration).NotNull().WithMessage("LicenseDateExpiration no puede ser nulo").WithErrorCode("600");
            RuleFor(s => s.LicenseNumber).NotNull().WithMessage("LicenseNumber no puede ser nulo").WithErrorCode("600");
        }
    }
}