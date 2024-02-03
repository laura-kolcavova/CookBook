using FluentValidation;

namespace CookBook.Recipes.Application.ValidationRules.Recipes;

public static class RecipePreparationTimeValidationRule
{
    public static IRuleBuilderOptionsConditions<T, short> RecipePreparationTime<T, TElement>(this IRuleBuilder<T, short> ruleBuilder)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            if (value < 0)
            {
                context.AddFailure("The recipe preparation time must not be less then 0");
            }
        });
    }
}
