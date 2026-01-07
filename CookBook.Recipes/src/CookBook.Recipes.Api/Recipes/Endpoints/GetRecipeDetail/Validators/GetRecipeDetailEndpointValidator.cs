using FluentValidation;

namespace CookBook.Recipes.Api.Recipes.Endpoints.GetRecipeDetail.Validators;

internal sealed class GetRecipeDetailEndpointValidator :
    AbstractValidator<GetRecipeDetailEndpointParams>
{
    public GetRecipeDetailEndpointValidator()
    {
        RuleFor(request => request.RecipeId)
            .NotNull()
            .GreaterThan(0);
    }
}
