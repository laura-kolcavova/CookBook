using CookBook.Recipes.Domain.Recipes.ReadModels;

namespace CookBook.Recipes.Api.Recipes.Features.GetRecipeDetail.Contracts;

internal sealed record GetRecipeDetailResponseDto
{
    public required RecipeDetailReadModel RecipeDetail { get; init; }
}
