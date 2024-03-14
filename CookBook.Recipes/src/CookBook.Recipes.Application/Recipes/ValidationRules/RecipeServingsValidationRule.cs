using FluentValidation;

namespace CookBook.Recipes.Application.Recipes.ValidationRules;

public static class RecipeServingsValidationRule
{
    public static IRuleBuilderOptionsConditions<T, short> RecipeServings<T>(this IRuleBuilder<T, short> ruleBuilder)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            if (value < 0)
            {
                context.AddFailure("The recipe servings must not be less then 0");
            }
        });
    }
}

