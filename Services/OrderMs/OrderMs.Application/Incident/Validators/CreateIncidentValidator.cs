using OrderMs.Common.Dtos.Request;
using FluentValidation;

namespace OrderMs.Application.Validators
{
    public class CreateIncidentValidator : ValidatorBase<CreateIncidentDto>
    {
        public CreateIncidentValidator()
        {
            RuleFor(s => s.Description).NotNull().NotEmpty().WithMessage("No puede ser nulo").WithErrorCode("654");
            RuleFor(s => s.DestinyLocation).NotNull().NotEmpty().WithMessage("No puede ser nulo").WithErrorCode("654");
            RuleFor(s => s.Date).NotNull().NotEmpty().WithMessage("No puede ser nulo").WithErrorCode("654");
        }
    }
}
