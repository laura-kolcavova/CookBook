using FluentValidation;

namespace CookBook.Recipes.Api.Recipes.Features.GetRecipeDetail.Validators;

internal sealed class GetRecipeDetailParamsValidator :
    AbstractValidator<GetRecipeDetailParams>
{
    public GetRecipeDetailParamsValidator()
    {
        RuleFor(request => request.RecipeId)
            .NotNull()
            .GreaterThan(0);
    }
}
