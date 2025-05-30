using CookBook.Recipes.Domain.Recipes.ReadModels;

namespace CookBook.Recipes.Api.Recipes.Features.SearchRecipes.Contracts;

internal record SearchRecipesEndpointResponseDto
{
    public required IReadOnlyCollection<RecipeListingItemReadModel> Recipes { get; init; }
}
