using CookBook.Recipes.Domain.Shared;

namespace CookBook.Recipes.Domain.Recipes.Entities;

public sealed class RecipeInstructionEntity : Entity
{
    public long RecipeId { get; }

    public int LocalId { get; }

    public string Note { get; private set; }

    public short OrderIndex { get; private set; }

    public RecipeInstructionEntity(int localId, string note)
    {
        LocalId = localId;
        Note = note;
    }

    public override object GetPrimaryKey() => new RecipeInstructionPrimaryKey
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
