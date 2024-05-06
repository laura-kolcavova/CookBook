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

    public required IEnumerable<int> CategoryIds { get; init; }

    public required IEnumerable<string> Tags { get; init; }

    public required string? Notes { get; init; }
}
