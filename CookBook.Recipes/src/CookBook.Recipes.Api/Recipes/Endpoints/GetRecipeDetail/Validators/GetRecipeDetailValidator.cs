using FluentValidation;

namespace CookBook.Recipes.Api.Recipes.Endpoints.GetRecipeDetail.Validators;

internal sealed class GetRecipeDetailValidator :
    AbstractValidator<GetRecipeDetailParams>
{
    public GetRecipeDetailValidator()
    {
        RuleFor(request => request.RecipeId)
            .NotNull()
            .GreaterThan(0);
    }
}
