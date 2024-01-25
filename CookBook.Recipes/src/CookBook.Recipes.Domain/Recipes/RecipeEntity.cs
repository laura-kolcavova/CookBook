using CookBook.Recipes.Domain.Common;

namespace CookBook.Recipes.Domain.Recipes;

public class RecipeEntity : IEntity<long>, ITrackableEntity
{
    public long Id { get; }

    public int UserId { get; private set; }

    public string Title { get; private set; }

    public string? Description { get; private set; }

    public DateTimeOffset? CreatedAt { get; private set; }

    public DateTimeOffset? UpdatedAt { get; private set; }

    #region NavigationProperties

    private readonly List<RecipeIngredientEntity> _ingredients;

    private readonly List<RecipeInstructionEntity> _instructions;

    public IReadOnlyCollection<RecipeIngredientEntity> Ingredients => _ingredients;

    public IReadOnlyCollection<RecipeInstructionEntity> Instructions => _instructions;

    #endregion NavigationProperties

    public RecipeEntity(int userId, string title, string? description)
    {
        UserId = userId;
        Title = title;
        Description = description;

        _ingredients = new List<RecipeIngredientEntity>();
        _instructions = new List<RecipeInstructionEntity>();
    }

    public RecipeEntity AddIngredient(string note, short orderIndex = 0)
    {
        var newIngredient = new RecipeIngredientEntity(note);

        newIngredient.SetOrderIndex(orderIndex);

        _ingredients.Add(newIngredient);

        return this;
    }

    public RecipeEntity RemoveIngredient(short ingredientId)
    {
        var index = _ingredients
            .FindIndex(ingredient => ingredient.Id == ingredientId);

        if (index >= 0)
        {
            _ingredients.RemoveAt(index);
        }

        return this;
    }

    public RecipeEntity RemoveAllIngredients()
    {
        _ingredients.Clear();
        return this;
    }

    public RecipeEntity AddInstruction(string note, short orderIndex = 0)
    {
        var newInstruction = new RecipeInstructionEntity(note);

        newInstruction.SetOrderIndex(orderIndex);

        _instructions.Add(newInstruction);

        return this;
    }

    public RecipeEntity RemoveInstruction(short instructionId)
    {
        var index = _instructions
            .FindIndex(instruction => instruction.Id == instructionId);

        if (index >= 0)
        {
            _instructions.RemoveAt(index);
        }

        return this;
    }

    public RecipeEntity RemoveAllInstructions()
    {
        _instructions.Clear();
        return this;
    }
}
