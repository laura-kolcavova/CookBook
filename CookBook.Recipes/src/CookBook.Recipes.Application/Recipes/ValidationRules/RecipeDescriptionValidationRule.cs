using FluentValidation;

namespace CookBook.Recipes.Application.Recipes.ValidationRules;

public static class RecipeDescriptionValidationRule
{
    public static IRuleBuilderOptionsConditions<T, string?> RecipeDescription<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            if (value is null)
            {
                return;
            }

            if (value.Length > 1024)
            {
                context.AddFailure("The recipe description must not exceed 1024 characters");
            }
        });
    }
}
