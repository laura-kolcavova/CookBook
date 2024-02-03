using FluentValidation;

namespace CookBook.Recipes.Application.ValidationRules.Recipes;

public static class RecipeIngredientNoteValidationRule
{
    public static IRuleBuilderOptionsConditions<T, string> RecipeIngredientNote<T, TElement>(this IRuleBuilder<T, string> ruleBuilder)
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
