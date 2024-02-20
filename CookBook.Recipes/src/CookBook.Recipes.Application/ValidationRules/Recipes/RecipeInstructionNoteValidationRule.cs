using FluentValidation;

namespace CookBook.Recipes.Application.ValidationRules.Recipes;

public static class RecipeInstructionNoteValidationRule
{
    public static IRuleBuilderOptionsConditions<T, string> RecipeInstructionNote<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            if (value.Length > 1024)
            {
                context.AddFailure("The recipe ingredient note must not exceed 1024 characters");
            }
        });
    }
}
