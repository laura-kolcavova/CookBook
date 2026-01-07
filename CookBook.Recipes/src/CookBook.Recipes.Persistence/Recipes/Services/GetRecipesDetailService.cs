using CookBook.Recipes.Domain.Recipes.ReadModels;
using CookBook.Recipes.Domain.Recipes.Services;
using CookBook.Recipes.Persistence.Recipes.Extensions;
using CookBook.Recipes.Persistence.Shared.Exceptions;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Persistence.Recipes.Services;

internal sealed class GetRecipesDetailService(
    RecipesContext recipesContext,
    ILogger<GetRecipesDetailService> logger) :
    IGetRecipeDetailService
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
                .ProjectToRecipeDetailReadModel()
                .SingleOrDefaultAsync(recipeDetail =>
                    recipeDetail.Id == recipeId,
                    cancellationToken);

            return readModel;
        }
        catch (Exception ex)
        {
            throw RecipesPersistenceException.LogAndCreate(
                logger,
                ex,
                "An unexpected error occurred while getting recipe detail");
        }
    }
}
