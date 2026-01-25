using CookBook.Recipes.Domain.Recipes.ReadModels;
using CookBook.Recipes.Domain.Recipes.UseCases.Abstractions;
using CookBook.Recipes.Persistence.Recipes.Extensions;
using CookBook.Recipes.Persistence.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Persistence.Recipes.UseCases;

internal sealed class GetLatestRecipesUseCase(
    RecipesContext recipesContext,
    ILogger<GetLatestRecipesUseCase> logger) : IGetLatestRecipesUseCase
{
    public async Task<IReadOnlyCollection<LatestRecipeReadModel>> GetLatestRecipes(
        int count,
        CancellationToken cancellationToken)
    {
        using var loggerScope = logger.BeginScope(new Dictionary<string, object?>
        {
            ["Count"] = count
        });

        try
        {
            var readModel = await recipesContext
                .Recipes
                .AsNoTracking()
                .OrderByDescending(recipe => recipe.CreatedAt)
                .ProjectToLatestRecipeReadModel()
                .Take(count)
                .ToListAsync(cancellationToken);

            return readModel;
        }
        catch (Exception ex)
        when (ex is not TaskCanceledException)
        {
            throw RecipesPersistenceException.LogAndCreate(
                logger,
                ex,
                "An unexpected error occurred while getting latest recipes");
        }
    }
}
