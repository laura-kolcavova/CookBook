using CookBook.Recipes.Api.Recipes.Features.GetRecipeDetail.Contracts;
using FluentValidation;

namespace CookBook.Recipes.Api.Recipes.Features.GetRecipeDetail.Validators;

internal sealed class GetRecipeDetailRequestValidator : AbstractValidator<GetRecipeDetailRequestDto>
{
    public GetRecipeDetailRequestValidator()
    {
        RuleFor(request => request.RecipeId)
            .NotNull()
            .GreaterThan(0);
    }
}
