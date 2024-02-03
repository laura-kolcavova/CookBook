﻿using FluentValidation;

namespace CookBook.Recipes.Application.ValidationRules.Recipes;

public static class RecipeTitleValidationRule
{
    public static IRuleBuilderOptionsConditions<T, string> RecipeTitle<T, TElement>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            if (value.Length < 3 || value.Length > 256)
            {
                context.AddFailure("The recipe title must be in between 3 and 256 characters");
            }
        });
    }
}
