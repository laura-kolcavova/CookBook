using CookBook.Recipes.Domain.Common;

namespace CookBook.Recipes.Domain.Entities.Recipes;

public class RecipeAggregate : AggregateRoot<long>, ITrackableEntity
{
    public int UserId { get; }

    public string Title { get; private set; }

    public string? Description { get; private set; }

    public string? Notes { get; private set; }

    public short Servings { get; private set; }

    public short PreparationTime { get; private set; }

    public short CookTime { get; private set; }

    public DateTimeOffset? CreatedAt { get; private set; }

    public DateTimeOffset? UpdatedAt { get; private set; }

    #region NavigationProperties

    private readonly List<RecipeIngredientEntity> _ingredients;

    private readonly List<RecipeInstructionEntity> _instructions;

    public IReadOnlyCollection<RecipeIngredientEntity> Ingredients => _ingredients;

    public IReadOnlyCollection<RecipeInstructionEntity> Instructions => _instructions;

    #endregion NavigationProperties

    public RecipeAggregate(int userId)
    {
        UserId = userId;

        Title = string.Empty;

        _ingredients = new List<RecipeIngredientEntity>();
        _instructions = new List<RecipeInstructionEntity>();
    }

    public void SetTitle(string title)
    {
        Title = title;
    }

    public void SetDescription(string? description)
    {
        Description = description;
    }

    public void SetNotes(string? notes)
    {
        Notes = notes;
    }

    public void SetServings(short servings)
    {
        Servings = servings;
    }

    public void SetPreparationTime(short preparationTime)
    {
        PreparationTime = preparationTime;
    }

    public void SetCookTime(short cookTime)
    {
        CookTime = cookTime;
    }

    public void SaveIngredients(SaveIngredientsParameters saveIngredientsParameters)
    {
        _ingredients.Clear();

        short orderIndex = 10;
        foreach (var ingredientParameters in saveIngredientsParameters.Ingredients)
        {
            var ingredient = new RecipeIngredientEntity(ingredientParameters.Id);

            ingredient.SetNote(ingredientParameters.Note);
            ingredient.SetOrderIndex(orderIndex);

            _ingredients.Add(ingredient);

            orderIndex++;
        }
    }

    public void SaveInstructions(SaveInstructionsParameters saveInstructionsParameters)
    {
        _ingredients.Clear();

        short orderIndex = 10;
        foreach (var instructionParameters in saveInstructionsParameters.Instructions)
        {
            var ingredient = new RecipeIngredientEntity(instructionParameters.Id);

            ingredient.SetNote(instructionParameters.Note);
            ingredient.SetOrderIndex(orderIndex);

            _ingredients.Add(ingredient);

            orderIndex++;
        }
    }
}
