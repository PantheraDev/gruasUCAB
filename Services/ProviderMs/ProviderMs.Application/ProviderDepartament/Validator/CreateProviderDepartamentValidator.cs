using ProviderMs.Common.dto.Request;
using FluentValidation;

namespace ProviderMs.Application.Validators
{
    public class CreateProviderDepartmentValidator : ValidatorBase<CreateProviderDepartmentdto>
    {
        public CreateProviderDepartmentValidator()
        {
            RuleFor(s => s.ProviderId).NotNull().NotEmpty().WithMessage("No puede ser nulo").WithErrorCode("654");
            RuleFor(s => s.DepartmentId).NotNull().NotEmpty().WithMessage("No puede ser nulo").WithErrorCode("654");
        }
    }
}