using FluentValidation;

namespace CookBook.Recipes.Api.Recipes.Features.GetRecipeDetail;

internal sealed class GetRecipeDetailRequestValidator : AbstractValidator<GetRecipeDetailRequestDto>
{
    public GetRecipeDetailRequestValidator()
    {
        RuleFor(request => request.RecipeId)
            .NotNull()
            .GreaterThan(0);
    }
}
