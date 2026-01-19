namespace CookBook.Recipes.Api.Recipes.Endpoints.SearchRecipes.Contracts;

internal sealed record SearchRecipesResponseDto
{
    public required IReadOnlyCollection<RecipeSearchItemDto> Recipes { get; init; }
}
