using FluentValidation;

namespace CookBook.Recipes.Application.ValidationRules;

public static class OrderIndexValidationRule
{
    public static IRuleBuilderOptionsConditions<T, short> OrderIndex<T>(this IRuleBuilder<T, short> ruleBuilder)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            if (value < 0)
            {
                context.AddFailure("The order index must not be less then 0");
            }
        });
    }
}
