using FluentValidation;

namespace CookBook.Recipes.Application.ValidationRules.Recipes;

public static class RecipeNotesValidationRule
{
    public static IRuleBuilderOptionsConditions<T, string?> RecipeNotes<T, TElement>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            if (value is null)
            {
                return;
            }

            if (value.Length > 1024)
            {
                context.AddFailure("The recipe notes must not exceed 1024 characters");
            }
        });
    }
}
