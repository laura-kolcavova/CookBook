namespace CookBook.Recipes.Domain.Recipes.ReadModels;

public sealed record RecipeSearchItemReadModel
{
    public required long RecipeId { get; init; }

    public required string Title { get; init; }

    public required DateTimeOffset CreatedAt { get; init; }
}
