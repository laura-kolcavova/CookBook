using CookBook.Recipes.Domain.Shared;

namespace CookBook.Recipes.Domain.Recipes.Entities;

public sealed class RecipeIngredientEntity : Entity
{
    public long RecipeId { get; }

    public int LocalId { get; }

    public string Note { get; private set; } = string.Empty;

    public short OrderIndex { get; private set; }

    public RecipeIngredientEntity(int localId, string note)
    {
        LocalId = localId;
        Note = note;
    }

    public override object GetPrimaryKey() => new RecipeIngredientPrimaryKey
    {
        RecipeId = RecipeId,
        LocalId = LocalId,
    };

    public void SetNote(string note)
    {
        Note = note;
    }

    public void SetOrderIndex(short orderIndex)
    {
        OrderIndex = orderIndex;
    }
}
