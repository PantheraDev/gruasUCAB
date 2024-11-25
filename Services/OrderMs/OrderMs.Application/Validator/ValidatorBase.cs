//using OrderMs.Infrastructure.Exceptions;
using FluentValidation;
using OrderMs.Infrastructure.Exceptions;

namespace OrderMs.Application.Validators
{
    public class ValidatorBase<T> : AbstractValidator<T>
    {
        public virtual async Task<bool> ValidateRequest(T request)
        {
            var result = await ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ValidatorException(string.Join(";\n", result.Errors.GroupBy(x => x.PropertyName).Select(g => $"{g.Key}: {string.Join(", ", g.Select(x => x.ErrorMessage))}")) ?? string.Empty);
            }

            return result.IsValid;
        }
    }
}
