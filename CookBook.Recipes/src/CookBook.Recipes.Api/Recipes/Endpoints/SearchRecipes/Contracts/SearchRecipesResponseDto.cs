using CookBook.Recipes.Domain.Recipes.ReadModels;

namespace CookBook.Recipes.Api.Recipes.Endpoints.SearchRecipes.Contracts;

internal sealed record SearchRecipesResponseDto
{
    public required IReadOnlyCollection<RecipeListingItemReadModel> Recipes { get; init; }
}
