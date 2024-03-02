namespace CookBook.Recipes.Api.Features.Recipes.Shared.Dto;

internal record RecipeListingItemDto
{
    public required long Id { get; init; }

    public required string Title { get; init; }
}
