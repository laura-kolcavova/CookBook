namespace CookBook.Recipes.Api.Recipes.Endpoints.SaveRecipe.Contracts;

internal sealed record SaveRecipeRequestDto
{
    public long? RecipeId { get; init; }

    public required int UserId { get; init; }

    public required string Title { get; init; }

    public string? Description { get; init; }

    public required short Servings { get; init; }

    public required short PreparationTime { get; init; }

    public required short CookTime { get; init; }

    public string? Notes { get; init; }

    public required IReadOnlyCollection<IngredientItem> Ingredients { get; init; }

    public required IReadOnlyCollection<InstructionItem> Instructions { get; init; }

    public required IReadOnlyCollection<string> Tags { get; init; }

    public sealed record IngredientItem
    {
        public int? LocalId { get; init; }

        public required string Note { get; init; }
    }

    public sealed record InstructionItem
    {
        public int? LocalId { get; init; }

        public required string Note { get; init; }
    }
}
