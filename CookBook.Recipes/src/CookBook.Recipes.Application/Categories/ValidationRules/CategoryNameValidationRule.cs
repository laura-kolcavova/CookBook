using FluentValidation;

namespace CookBook.Recipes.Application.Categories.ValidationRules;

public static class CategoryNameValidationRule
{
    public static IRuleBuilderOptionsConditions<T, string?> CategoryName<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .Custom((value, context) =>
            {
                if (value is null)
                {
                    return;
                }

                if (value.Length < 3 || value.Length > 256)
                {
                    context.AddFailure("The category name must be in between 3 and 256 characters");
                }
            });
    }
}
