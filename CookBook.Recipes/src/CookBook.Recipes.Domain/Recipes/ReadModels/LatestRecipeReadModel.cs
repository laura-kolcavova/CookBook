namespace CookBook.Recipes.Domain.Recipes.ReadModels;

public sealed record LatestRecipeReadModel
{
    public required long Id { get; init; }

    public required string Title { get; init; }

    public required string? Description { get; init; }

    public required DateTimeOffset CreatedAt { get; init; }

    public required string ImageUrl { get; init; }
}
