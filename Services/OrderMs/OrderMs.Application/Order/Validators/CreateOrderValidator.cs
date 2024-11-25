using OrderMs.Common.Dtos.Request;
using FluentValidation;

namespace OrderMs.Application.Validators
{
    public class CreateOrderValidator : ValidatorBase<CreateOrderDto>
    {
        public CreateOrderValidator()
        {
            RuleFor(s => s.DestinyLocation).NotNull().NotEmpty().WithMessage("No puede ser nulo").WithErrorCode("654");
            RuleFor(s => s.TotalCost).NotNull().NotEmpty().WithMessage("No puede ser nulo").WithErrorCode("654");
            RuleFor(s => s.Date).NotNull().NotEmpty().WithMessage("No puede ser nulo").WithErrorCode("654");
            RuleFor(s => s.IncidentId).NotNull().NotEmpty().WithMessage("No puede ser nulo").WithErrorCode("654");
            RuleFor(s => s.PolicyId).NotNull().NotEmpty().WithMessage("No puede ser nulo").WithErrorCode("654");
        }
    }
}
