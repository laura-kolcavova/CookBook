﻿using CookBook.Recipes.Domain.Shared;

namespace CookBook.Recipes.Domain.Recipes;

public sealed class RecipeIngredientEntity : Entity<long>, ITrackableEntity
{
    public long RecipeId { get; }

    public string Note { get; private set; } = string.Empty;

    public short OrderIndex { get; private set; }

    public DateTimeOffset? CreatedAt { get; private set; }

    public DateTimeOffset? UpdatedAt { get; private set; }

    public RecipeIngredientEntity()
    {
    }

    public RecipeIngredientEntity SetNote(string note)
    {
        Note = note;
        return this;
    }

    public RecipeIngredientEntity SetOrderIndex(short orderIndex)
    {
        OrderIndex = orderIndex;
        return this;
    }
}
