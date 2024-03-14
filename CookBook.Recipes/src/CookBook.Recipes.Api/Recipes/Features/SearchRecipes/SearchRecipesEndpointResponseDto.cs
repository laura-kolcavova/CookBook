using CookBook.Recipes.Domain.Recipes.ReadModels;

namespace CookBook.Recipes.Api.Recipes.Features.SearchRecipes;

internal record SearchRecipesEndpointResponseDto
{
    public required IReadOnlyCollection<RecipeListingItemReadModel> Recipes { get; init; }
}
