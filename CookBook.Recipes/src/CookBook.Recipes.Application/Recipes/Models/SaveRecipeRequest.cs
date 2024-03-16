using CookBook.Recipes.Domain.Recipes.Parameters;

namespace CookBook.Recipes.Application.Recipes.Models;

public sealed record SaveRecipeRequest
{
    public required long? RecipeId { get; init; }

    public required int UserId { get; init; }

    public required string Title { get; init; }

    public required string? Description { get; init; }

    public required short Servings { get; init; }

    public required short PreparationTime { get; init; }

    public required short CookTime { get; init; }

    public required SaveIngredientsParameters Ingredients { get; init; }

    public required SaveInstructionsParameters Instructions { get; init; }

    public required string? Notes { get; init; }

    public record IngredientItem
    {
        public required long? Id { get; init; }

        public required string Note { get; init; }
    }

    public record InstructionItem
    {
        public required long? Id { get; init; }

        public required string Note { get; init; }
    }
}
