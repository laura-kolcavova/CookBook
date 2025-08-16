using FluentValidation;

namespace CookBook.Recipes.Api.Recipes.Features.SaveRecipe.Validators;

internal sealed class SaveRecipeParamsValidator :
    AbstractValidator<SaveRecipeParams>
{
    public SaveRecipeParamsValidator()
    {
        RuleFor(request => request.SaveRecipeRequest)
            .NotNull()
            .SetValidator(new SaveRecipeRequestValidator());
    }
}
