using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.Recipes.Domain.Recipes;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Application.Features.Recipes.SaveRecipe;

internal class SaveRecipeCommandHandler : IRequestHandler<SaveRecipeCommand, Result<long, ExpectedError>>
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly ILogger<SaveRecipeCommandHandler> _logger;

    public SaveRecipeCommandHandler(
        IRecipeRepository recipeRepository, ILogger<SaveRecipeCommandHandler> logger)
    {
        _recipeRepository = recipeRepository;
        _logger = logger;
    }

    public async Task<Result<long, ExpectedError>> Handle(SaveRecipeCommand request, CancellationToken cancellationToken)
    {
        using var loggerScope = _logger.BeginScope(new Dictionary<string, object>
        {
            ["RecipeId"] = request.RecipeId,
            ["UserId"] = request.UserId,
            ["Title"] = request.Title,
        });

        try
        {
            var recipe = request.RecipeId <= 0
                ? null
                : await _recipeRepository
                .GetOneAsync(request.RecipeId, cancellationToken);

            if (recipe is null)
            {
                return await AddNewRecipe(request, cancellationToken);
            }
            else
            {
                return await EditRecipeInformation(recipe, request, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while saving a recipe");
            throw;
        }
    }

    private async Task<Result<long, ExpectedError>> AddNewRecipe(SaveRecipeCommand request, CancellationToken cancellationToken)
    {
        var newRecipe = new RecipeAggregate(request.UserId);

        newRecipe.SetTitle(request.Title);
        newRecipe.SetDescription(request.Description);
        newRecipe.SetServings(request.Servings);
        newRecipe.SetPreparationTime(request.PreparationTime);
        newRecipe.SetCookTime(request.ServingsTime);
        newRecipe.SetNotes(request.Notes);

        newRecipe.SaveIngredients(new SaveIngredientsParameters
        {
            Ingredients = request.Ingredients.Select(ingredient => new SaveIngredientsParameters.IngredientParameters
            {
                Note = ingredient.Note,
            })
        });

        newRecipe.SaveInstructions(new SaveInstructionsParameters
        {
            Instructions = request.Instructions.Select(instruction => new SaveInstructionsParameters.InstructionParameters
            {
                Note = instruction.Note,
            })
        });

        await _recipeRepository.AddAsync(newRecipe, cancellationToken);

        await _recipeRepository.SaveChangesAsync(cancellationToken);

        return newRecipe.Id;
    }

    private async Task<Result<long, ExpectedError>> EditRecipeInformation(RecipeAggregate recipe, SaveRecipeCommand request, CancellationToken cancellationToken)
    {
        recipe.SetTitle(request.Title);
        recipe.SetDescription(request.Description);
        recipe.SetServings(request.Servings);
        recipe.SetPreparationTime(request.PreparationTime);
        recipe.SetCookTime(request.ServingsTime);
        recipe.SetNotes(request.Notes);

        recipe.SaveIngredients(new SaveIngredientsParameters
        {
            Ingredients = request.Ingredients.Select(ingredient => new SaveIngredientsParameters.IngredientParameters
            {
                Id = ingredient.Id,
                Note = ingredient.Note,
            })
        });

        recipe.SaveInstructions(new SaveInstructionsParameters
        {
            Instructions = request.Instructions.Select(instruction => new SaveInstructionsParameters.InstructionParameters
            {
                Id = instruction.Id,
                Note = instruction.Note,
            })
        });

        _recipeRepository.Update(recipe);

        await _recipeRepository.SaveChangesAsync(cancellationToken);

        return recipe.Id;
    }
}
