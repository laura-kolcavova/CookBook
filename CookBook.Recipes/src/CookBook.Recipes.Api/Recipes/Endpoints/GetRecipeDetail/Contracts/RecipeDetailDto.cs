namespace CookBook.Recipes.Api.Recipes.Endpoints.GetRecipeDetail.Contracts;

internal sealed record RecipeDetailDto
{
    public required long Id { get; init; }

    public required int UserId { get; init; }

    public required string Title { get; init; }

    public required string? Description { get; init; }

    public required short Servings { get; init; }

    public required short CookTime { get; init; }

    public required string? Notes { get; init; }

    public required DateTimeOffset CreatedAt { get; init; }

    public required IReadOnlyCollection<IngredientItem> Ingredients { get; init; }

    public required IReadOnlyCollection<InstructionItem> Instructions { get; init; }

    public required IReadOnlyCollection<string> Tags { get; init; }

    public sealed record IngredientItem
    {
        public required int LocalId { get; init; }

        public required string Note { get; init; }
    }

    public sealed record InstructionItem
    {
        public required int LocalId { get; init; }

        public required string Note { get; init; }
    }
}
