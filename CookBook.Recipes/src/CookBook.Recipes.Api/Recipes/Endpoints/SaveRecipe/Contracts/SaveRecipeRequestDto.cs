namespace CookBook.Recipes.Api.Recipes.Endpoints.SaveRecipe.Contracts;

internal sealed record SaveRecipeRequestDto
{
    public long? RecipeId { get; init; }

    public required string Title { get; init; }

    public string? Description { get; init; }

    public required short Servings { get; init; }

    public required short CookTime { get; init; }

    public string? Notes { get; init; }

    public required IReadOnlyCollection<IngredientItemDto> Ingredients { get; init; }

    public required IReadOnlyCollection<InstructionItemDto> Instructions { get; init; }

    public required IReadOnlyCollection<string> Tags { get; init; }

    public sealed record IngredientItemDto
    {
        public int? LocalId { get; init; }

        public required string Note { get; init; }
    }

    public sealed record InstructionItemDto
    {
        public int? LocalId { get; init; }

        public required string Note { get; init; }
    }
}
