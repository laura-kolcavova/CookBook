﻿using CookBook.Recipes.Domain.Recipes.Models;

namespace CookBook.Recipes.Application.Recipes.Models;

public sealed record SaveRecipeRequestModel
{
    public required long? RecipeId { get; init; }

    public required int UserId { get; init; }

    public required string Title { get; init; }

    public required string? Description { get; init; }

    public required short Servings { get; init; }

    public required short PreparationTime { get; init; }

    public required short CookTime { get; init; }

    public required IReadOnlyCollection<SaveIngredientItem> Ingredients { get; init; }

    public required IReadOnlyCollection<SaveInstructionItem> Instructions { get; init; }

    public required IReadOnlyCollection<string> Tags { get; init; }

    public required string? Notes { get; init; }
}
