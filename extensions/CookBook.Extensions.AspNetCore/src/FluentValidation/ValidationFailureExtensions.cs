using CookBook.Extensions.AspNetCore.FluentValidation;
using FluentValidation.Results;

namespace CookBook.Extensions.AspNetCore.FluentValidation;

public static class ValidationFailureExtensions
{
    public static IDictionary<string, string[]> ToDictionary(this
        IEnumerable<ValidationFailure> validationFailures)
    {
        return validationFailures
          .GroupBy(x => x.PropertyName)
          .ToDictionary(
            g => g.Key,
            g => g.Select(x => x.ErrorMessage).ToArray()
          );
    }
}
