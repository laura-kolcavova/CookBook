using CookBook.Recipes.Domain.Recipes.Parameters;
using CookBook.Recipes.Domain.Shared;

namespace CookBook.Recipes.Domain.Recipes;

public sealed class RecipeAggregate : AggregateRoot<long>, ITrackableEntity
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
        var newIngredients = new List<RecipeIngredientEntity>();
        short orderIndex = 10;

        foreach (var ingredientParameter in saveIngredientsParameters.Ingredients)
        {
            var ingredient = GetOrCreateIngredient(ingredientParameter.Id);

            ingredient.SetNote(ingredientParameter.Note);
            ingredient.SetOrderIndex(orderIndex);

            newIngredients.Add(ingredient);

            orderIndex += 10;
        }

        _ingredients.Clear();
        _ingredients.AddRange(newIngredients);
    }

    public void SaveInstructions(SaveInstructionsParameters saveInstructionsParameters)
    {
        var newInstructions = new List<RecipeInstructionEntity>();
        short orderIndex = 10;

        foreach (var instructionParameter in saveInstructionsParameters.Instructions)
        {
            var instruction = GetOrCreateInstruction(instructionParameter.Id);

            instruction.SetNote(instructionParameter.Note);
            instruction.SetOrderIndex(orderIndex);

            newInstructions.Add(instruction);

            orderIndex += 10;
        }

        _instructions.Clear();
        _instructions.AddRange(newInstructions);
    }

    private RecipeIngredientEntity GetOrCreateIngredient(long? ingredientId)
    {
        var ingredient = ingredientId is null || ingredientId <= 0
            ? null
            : _ingredients.FirstOrDefault(ingredient => ingredient.Id == ingredientId);

        if (ingredient is null)
        {
            return new RecipeIngredientEntity();
        }

        return ingredient;
    }

    private RecipeInstructionEntity GetOrCreateInstruction(long? instructionId)
    {
        var instruction = instructionId is null || instructionId <= 0
            ? null
            : _instructions.FirstOrDefault(instruction => instruction.Id == instructionId);

        if (instruction is null)
        {
            return new RecipeInstructionEntity();
        }

        return instruction;
    }
}
