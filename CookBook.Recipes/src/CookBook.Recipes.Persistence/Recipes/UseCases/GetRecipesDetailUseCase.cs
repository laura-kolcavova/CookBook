using CookBook.Recipes.Domain.Recipes.ReadModels;
using CookBook.Recipes.Domain.Recipes.UseCases.Abstractions;
using CookBook.Recipes.Persistence.Recipes.Extensions;
using CookBook.Recipes.Persistence.Shared.Exceptions;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Persistence.Recipes.UseCases;

internal sealed class GetRecipesDetailUseCase(
    RecipesContext recipesContext,
    ILogger<GetRecipesDetailUseCase> logger) :
    IGetRecipeDetailUseCase
{
    public async Task<Maybe<RecipeDetailReadModel>> GetRecipeDetail(
        long recipeId,
        CancellationToken cancellationToken)
    {
        using var loggerScope = logger.BeginScope(new Dictionary<string, object?>
        {
            ["RecipeId"] = recipeId
        });

        try
        {
            var readModel = await recipesContext
                .Recipes
                .AsNoTracking()
                .Where(
                    recipe => recipe.Id == recipeId)
                .ProjectToRecipeDetailReadModel()
                .SingleOrDefaultAsync(
                    cancellationToken);

            return readModel;
        }
        catch (Exception ex)
        when (ex is not TaskCanceledException)
        {
            throw RecipesPersistenceException.LogAndCreate(
                logger,
                ex,
                "An unexpected error occurred while getting recipe detail");
        }
    }
}
