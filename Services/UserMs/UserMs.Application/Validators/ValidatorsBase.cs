using UserMs.Infrastructure.Exceptions;
using FluentValidation;

namespace UserMs.Application.Validators
{
    public class ValidatorBase<T> : AbstractValidator<T>
    {
        public virtual async Task<bool> ValidateRequest(T request)
        {
            var result = await ValidateAsync(request);
            if (!result.IsValid)
            {
                var errorMessage = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                throw new ValidatorException(errorMessage);
            }

            return result.IsValid;
        }
    }
}