namespace CookBook.Recipes.Domain.Recipes.ReadModels;

public sealed record RecipeListingItemReadModel
{
    public required long Id { get; init; }

    public required string Title { get; init; }

    public required DateTimeOffset CreatedAt { get; init; }
}
