using OrderMs.Common.Dtos.Request;
using FluentValidation;

namespace OrderMs.Application.Validators
{
    public class CreateFeeValidator : ValidatorBase<CreateFeeDto>
    {
        public CreateFeeValidator()
        {
            RuleFor(s => s.BasePrice).NotNull().NotEmpty().WithMessage("No puede ser nulo").WithErrorCode("654");
            RuleFor(s => s.Radius).NotNull().NotEmpty().WithMessage("No puede ser nulo").WithErrorCode("654");
            RuleFor(s => s.PriceXKm).NotNull().NotEmpty().WithMessage("No puede ser nulo").WithErrorCode("654");
        }
    }
}
