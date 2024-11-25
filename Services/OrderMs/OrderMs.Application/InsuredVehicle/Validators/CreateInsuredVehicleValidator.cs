using OrderMs.Common.Dtos.Request;
using FluentValidation;

namespace OrderMs.Application.Validators
{
    public class CreateInsuredVehicleValidator : ValidatorBase<CreateInsuredVehicleDto>
    {
        public CreateInsuredVehicleValidator()
        {
            RuleFor(s => s.Weight).NotNull().NotEmpty().WithMessage("No puede ser nulo").WithErrorCode("654");
            RuleFor(s => s.LicensePlate).NotNull().NotEmpty().WithMessage("No puede ser nulo").WithErrorCode("654");
            RuleFor(s => s.Brand).NotNull().NotEmpty().WithMessage("No puede ser nulo").WithErrorCode("654");
            RuleFor(s => s.Model).NotNull().NotEmpty().WithMessage("No puede ser nulo").WithErrorCode("654");
            RuleFor(s => s.Year).NotNull().NotEmpty().WithMessage("No puede ser nulo").WithErrorCode("654");
            RuleFor(s => s.Color).NotNull().NotEmpty().WithMessage("No puede ser nulo").WithErrorCode("654");
            RuleFor(s => s.ClientId).NotNull().NotEmpty().WithMessage("No puede ser nulo").WithErrorCode("654");
        }
    }
}
