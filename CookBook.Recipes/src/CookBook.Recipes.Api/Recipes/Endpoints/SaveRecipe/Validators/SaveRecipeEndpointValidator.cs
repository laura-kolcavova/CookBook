using FluentValidation;

namespace CookBook.Recipes.Api.Recipes.Endpoints.SaveRecipe.Validators;

internal sealed class SaveRecipeEndpointValidator :
    AbstractValidator<SaveRecipeEndpointParams>
{
    public SaveRecipeEndpointValidator()
    {
        RuleFor(request => request.SaveRecipeRequest)
            .NotNull()
            .SetValidator(new SaveRecipeRequestDtoValidator());
    }
}
