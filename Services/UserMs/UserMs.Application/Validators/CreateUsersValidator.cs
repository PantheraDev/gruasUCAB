using UserMs.Application.Dtos.Users.Request;
using FluentValidation;

namespace UserMs.Application.Validators
{
    public class CreateUsersValidator : ValidatorBase<CreateUsersDto>
    {
        public CreateUsersValidator()
        {
            RuleFor(s => s.UserEmail).NotNull().WithMessage("UserEmail no puede ser nulo").WithErrorCode("600");
            RuleFor(s => s.UserPassword).NotNull().WithMessage("UserPassword no puede ser nulo").WithErrorCode("600");
            RuleFor(s => s.UsersType).NotNull().WithMessage("UsersType no puede ser nulo").WithErrorCode("600");
        }
    }
}