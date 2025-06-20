﻿namespace CookBook.Recipes.Api.Recipes.Features.SaveRecipe.Contracts;

internal sealed class SaveRecipeRequestDto
{
    public long? RecipeId { get; init; }

    public int UserId { get; init; }

    public string Title { get; init; } = string.Empty;

    public string? Description { get; init; }

    public short Servings { get; init; }

    public short PreparationTime { get; init; }

    public short CookTime { get; init; }

    public string? Notes { get; init; }

    public IReadOnlyCollection<IngredientItem> Ingredients { get; init; } = new List<IngredientItem>();

    public IReadOnlyCollection<InstructionItem> Instructions { get; init; } = new List<InstructionItem>();

    public IReadOnlyCollection<string> Tags { get; init; } = new List<string>();

    public sealed class IngredientItem
    {
        public int? LocalId { get; init; }

        public string Note { get; init; } = string.Empty;
    }

    public sealed class InstructionItem
    {
        public int? LocalId { get; init; }

        public string Note { get; init; } = string.Empty;
    }
}
