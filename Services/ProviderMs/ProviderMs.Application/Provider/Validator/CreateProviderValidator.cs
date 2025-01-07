using ProviderMs.Common.dto.Request;
using FluentValidation;

namespace ProviderMs.Application.Validators
{
    public class CreateProviderValidator : ValidatorBase<CreateProviderdto>
    {
        public CreateProviderValidator()
        {
            RuleFor(s => s.Name).NotNull().NotEmpty().WithMessage("No puede ser nulo").WithErrorCode("654");
            RuleFor(s => s.PhoneNumber).NotNull().NotEmpty().WithMessage("No puede ser nulo").WithErrorCode("654");
            RuleFor(s => s.Email).NotNull().NotEmpty().WithMessage("No puede ser nulo").WithErrorCode("654");
            RuleFor(s => s.RIF).NotNull().NotEmpty().WithMessage("No puede ser nulo").WithErrorCode("654");
            RuleFor(s => s.Address).NotNull().NotEmpty().WithMessage("No puede ser nulo").WithErrorCode("654");  
        }
    }
}