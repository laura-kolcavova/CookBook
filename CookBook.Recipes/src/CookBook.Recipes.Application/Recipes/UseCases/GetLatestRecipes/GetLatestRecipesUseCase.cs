using CookBook.Recipes.Domain.Recipes.ReadModels;
using CookBook.Recipes.Domain.Recipes.Services.Abstractions;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Application.Recipes.UseCases.GetLatestRecipes;

internal sealed class GetLatestRecipesUseCase(
    IGetLatestRecipesQuery getLatestRecipesQuery,
    ILogger<GetLatestRecipesUseCase> logger) :
    IGetLatestRecipesUseCase
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
            var latestRecipes = await getLatestRecipesQuery.Execute(
                count,
                cancellationToken);

            return latestRecipes;
        }
        catch (Exception ex)
        when (ex is not OperationCanceledException)
        {
            logger.LogError(
                ex,
                "An unexpected error occurred while getting latest recipes");

            throw;
        }
    }
}
