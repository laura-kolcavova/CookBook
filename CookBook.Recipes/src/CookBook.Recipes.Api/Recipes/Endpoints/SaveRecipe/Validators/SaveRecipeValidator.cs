using FluentValidation;

namespace CookBook.Recipes.Api.Recipes.Endpoints.SaveRecipe.Validators;

internal sealed class SaveRecipeValidator :
    AbstractValidator<SaveRecipeParams>
{
    public SaveRecipeValidator()
    {
        RuleFor(request => request.SaveRecipeRequest)
            .NotNull()
            .SetValidator(new SaveRecipeRequestDtoValidator());
    }
}
