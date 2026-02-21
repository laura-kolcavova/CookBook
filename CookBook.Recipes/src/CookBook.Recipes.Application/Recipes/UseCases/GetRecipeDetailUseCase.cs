using CookBook.Recipes.Application.Recipes.UseCases.Abstractions;
using CookBook.Recipes.Domain.Recipes.ReadModels;
using CookBook.Recipes.Domain.Recipes.Services.Abstractions;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Application.Recipes.UseCases;

internal sealed class GetRecipeDetailUseCase(
    IGetRecipeDetailQuery getRecipeDetailQuery,
    ILogger<GetRecipeDetailUseCase> logger) :
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
            var recipeDetail = await getRecipeDetailQuery.Execute(
                recipeId,
                cancellationToken);

            if (recipeDetail is null)
            {
                return Maybe<RecipeDetailReadModel>.None;
            }

            return recipeDetail;
        }
        catch (Exception ex)
        when (ex is not TaskCanceledException)
        {
            logger.LogError(
                ex,
                "An unexpected error occurred while getting recipe detail");

            throw;
        }
    }
}
