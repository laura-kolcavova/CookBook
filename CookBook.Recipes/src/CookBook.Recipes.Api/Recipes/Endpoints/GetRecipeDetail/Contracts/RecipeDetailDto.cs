namespace CookBook.Recipes.Api.Recipes.Endpoints.GetRecipeDetail.Contracts;

internal sealed record RecipeDetailDto
{
    public required long RecipeId { get; init; }

    public required string UserName { get; init; }

    public required string Title { get; init; }

    public required string? Description { get; init; }

    public required short Servings { get; init; }

    public required short CookTime { get; init; }

    public required string? Notes { get; init; }

    public required DateTimeOffset CreatedAt { get; init; }

    public required IReadOnlyCollection<IngredientItemDto> Ingredients { get; init; }

    public required IReadOnlyCollection<InstructionItemDto> Instructions { get; init; }

    public required IReadOnlyCollection<string> Tags { get; init; }

    public sealed record IngredientItemDto
    {
        public required int LocalId { get; init; }

        public required string Note { get; init; }
    }

    public sealed record InstructionItemDto
    {
        public required int LocalId { get; init; }

        public required string Note { get; init; }
    }
}
