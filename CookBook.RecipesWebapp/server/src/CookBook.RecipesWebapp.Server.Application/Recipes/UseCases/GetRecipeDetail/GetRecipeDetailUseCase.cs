using CookBook.RecipesWebapp.Server.Domain.Recipes.Models;
using CookBook.RecipesWebapp.Server.Domain.Recipes.Services.Abastractions;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace CookBook.RecipesWebapp.Server.Application.Recipes.UseCases.GetRecipeDetail;

internal sealed class GetRecipeDetailUseCase(
    IRecipeDetailFetcher recipeDetailFetcher,
    ILogger<GetRecipeDetailUseCase> logger) :
    IGetRecipeDetailUseCase
{
    public async Task<Maybe<RecipeDetailModel>> GetRecipeDetail(
        long recipeId,
        CancellationToken cancellationToken)
    {
        using var loggerScope = logger.BeginScope(new Dictionary<string, object>
        {
            ["RecipeId"] = recipeId,
        });

        try
        {
            var recipeDetailResult = await recipeDetailFetcher.FetchRecipeDetail(
                recipeId,
                cancellationToken);

            return recipeDetailResult;
        }
        catch (Exception ex)
        when (ex is not OperationCanceledException)
        {
            logger.LogError(
                ex,
                "An unexpected error occurred while getting recipe detail");

            throw;
        }
    }
}
