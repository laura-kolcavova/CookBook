namespace CookBook.Recipes.Api.Recipes.Endpoints.SearchRecipes.Contracts;

public class RecipeSearchItemDto
{
    public required long RecipeId { get; init; }

    public required string Title { get; init; }

    public required DateTimeOffset CreatedAt { get; init; }
}
