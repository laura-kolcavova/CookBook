using CookBook.Recipes.Api.Features.Recipes.Shared.Dto;

namespace CookBook.Recipes.Api.Features.Recipes.SearchRecipes;

internal record SearchRecipesEndpointResponse
{
    public required IReadOnlyCollection<RecipeListingItemDto> Recipes { get; init; }
}
