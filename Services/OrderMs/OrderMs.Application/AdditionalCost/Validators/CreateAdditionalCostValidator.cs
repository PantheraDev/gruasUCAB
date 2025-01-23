using OrderMs.Common.Dtos.Request;
using FluentValidation;

namespace OrderMs.Application.Validators
{
    public class CreateAdditionalCostValidator : ValidatorBase<CreateAdditionalCostDto>
    {
        public CreateAdditionalCostValidator()
        {
            RuleFor(s => s.Description).NotNull().NotEmpty().WithMessage("No puede ser nulo").WithErrorCode("654");
            RuleFor(s => s.Value).NotNull().NotEmpty().WithMessage("No puede ser nulo").WithErrorCode("654");
        }
    }
}
