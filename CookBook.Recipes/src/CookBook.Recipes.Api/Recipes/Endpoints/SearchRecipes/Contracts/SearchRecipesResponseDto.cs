namespace CookBook.Recipes.Api.Recipes.Endpoints.SearchRecipes.Contracts;

internal sealed record SearchRecipesResponseDto
{
    public required IReadOnlyCollection<RecipeSearchItemDto> Recipes { get; init; }

    public sealed record RecipeSearchItemDto
    {
        public required long RecipeId { get; init; }

        public required string Title { get; init; }

        public required DateTimeOffset CreatedAt { get; init; }

        public required string ImageUrl { get; init; }
    }
}
