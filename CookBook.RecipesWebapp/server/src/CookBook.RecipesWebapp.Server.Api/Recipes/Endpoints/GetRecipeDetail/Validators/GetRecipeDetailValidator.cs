using FluentValidation;

namespace CookBook.RecipesWebapp.Server.Api.Recipes.Endpoints.GetRecipeDetail.Validators;

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
