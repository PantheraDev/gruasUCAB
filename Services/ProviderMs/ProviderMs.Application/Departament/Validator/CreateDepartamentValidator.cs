using ProviderMs.Common.dto.Request;
using FluentValidation;
using ProviderMs.Application.Validators;

namespace ProviderMs.Application.Validators
{
    public class CreateDepartmentValidator : ValidatorBase<CreateDepartmentdto>
    {
        public CreateDepartmentValidator()
        {
            RuleFor(s => s.Name).NotNull().NotEmpty().WithMessage("No puede ser nulo").WithErrorCode("654");
        }
    }
}