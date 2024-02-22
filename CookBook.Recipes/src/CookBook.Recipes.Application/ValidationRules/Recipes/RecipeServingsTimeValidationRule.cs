using FluentValidation;

namespace CookBook.Recipes.Application.ValidationRules.Recipes;

public static class RecipeServingsTimeValidationRule
{
    public static IRuleBuilderOptionsConditions<T, short> RecipeServingsTime<T>(this IRuleBuilder<T, short> ruleBuilder)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            if (value < 0)
            {
                context.AddFailure("The recipe servings time must not be less then 0");
            }
        });
    }
}
