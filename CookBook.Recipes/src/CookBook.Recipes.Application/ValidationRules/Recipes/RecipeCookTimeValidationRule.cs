using FluentValidation;

namespace CookBook.Recipes.Application.ValidationRules.Recipes;

public static class RecipeCookTimeValidationRule
{
    public static IRuleBuilderOptionsConditions<T, short> RecipeCookTime<T, TElement>(this IRuleBuilder<T, short> ruleBuilder)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            if (value < 0)
            {
                context.AddFailure("The recipe cook time must not be less then 0");
            }
        });
    }
}
