using FluentValidation;

namespace CookBook.Recipes.Application.Recipes.ValidationRules;

public static class RecipeIngredientNoteValidationRule
{
    public static IRuleBuilderOptionsConditions<T, string> RecipeIngredientNote<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            if (value.Length > 256)
            {
                context.AddFailure("The recipe ingredient note must not exceed 256 characters");
            }
        });
    }
}
