﻿using CookBook.Extensions.CSharpExtended.Errors;
using CSharpFunctionalExtensions;
using MediatR;

namespace CookBook.Recipes.Application.Features.Recipes.SaveRecipe;

public record SaveRecipeCommand : IRequest<Result<long, ExpectedError>>
{
    public required long RecipeId { get; init; }

    public required int UserId { get; init; }

    public required string Title { get; init; }

    public required string? Description { get; init; }

    public required short Servings { get; init; }

    public required short PreparationTime { get; init; }

    public required short ServingsTime { get; init; }

    public required IReadOnlyCollection<IngredientItem> Ingredients { get; init; }

    public required IReadOnlyCollection<InstructionItem> Instructions { get; init; }

    public required string? Notes { get; init; }

    public record IngredientItem
    {
        public required long Id { get; init; }

        public required string Note { get; init; }
    }

    public record InstructionItem
    {
        public required long Id { get; init; }

        public required string Note { get; init; }
    }
}