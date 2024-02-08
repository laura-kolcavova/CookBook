using CookBook.Recipes.Domain.Common;

namespace CookBook.Recipes.Domain.Entities.Recipes;

public class RecipeIngredientEntity : Entity<long>, ITrackableEntity
{
    public long RecipeId { get; }

    public string Note { get; private set; } = string.Empty;

    public short OrderIndex { get; private set; }

    public DateTimeOffset? CreatedAt { get; private set; }

    public DateTimeOffset? UpdatedAt { get; private set; }

    public RecipeIngredientEntity()
    {
    }

    public RecipeIngredientEntity(long id)
        : base(id)
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
