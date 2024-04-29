using CookBook.Recipes.Api.Recipes.Features.SaveRecipe;
using CookBook.Recipes.Application.Recipes.Models;
using CookBook.Recipes.Domain.Recipes.Parameters;

namespace CookBook.Recipes.Api.Features.Recipes.SaveRecipe;

internal static class SaveRecipeMapper
{
    public static SaveRecipeRequest ToSaveRecipeRequest(SaveRecipeRequestDto request)
    {
        return new SaveRecipeRequest
        {
            RecipeId = request.RecipeId,
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description,
            Servings = request.Servings,
            PreparationTime = request.PreparationTime,
            CookTime = request.CookTime,
            Notes = request.Notes,
            Ingredients = new SaveIngredientsParameters
            {
                Ingredients = request.Ingredients
                .Select(ingredient => new SaveIngredientsParameters.IngredientParameters
                {
                    LocalId = ingredient.LocalId,
                    Note = ingredient.Note
                })
                .ToList()
            },
            Instructions = new SaveInstructionsParameters
            {
                Instructions = request.Instructions
                .Select(instruction => new SaveInstructionsParameters.InstructionParameters
                {
                    LocalId = instruction.LocalId,
                    Note = instruction.Note
                })
                .ToList()
            },
            CategoryIds = request.CategoryIds
                .ToList()
        };
    }

    public static SaveRecipeResponseDto ToResponse(SaveRecipeResult saveRecipeResult)
    {
        return new SaveRecipeResponseDto
        {
            RecipeId = saveRecipeResult.RecipeId
        };
    }
}
