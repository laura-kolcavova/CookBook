using CookBook.Recipes.Domain.Recipes.ReadModels;
using CookBook.Recipes.Domain.Recipes.Services.Abstractions;
using CookBook.Recipes.Infrastructure.Recipes.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Recipes.Infrastructure.Recipes.Services;

internal sealed class GetLatestRecipesQuery(
    RecipesContext recipesContext) :
    IGetLatestRecipesQuery
{
    public async Task<IReadOnlyCollection<LatestRecipeReadModel>> Execute(
        int count,
        CancellationToken cancellationToken)
    {
        var latestRecipes = await recipesContext
            .Recipes
            .AsNoTracking()
            .OrderByDescending(recipe => recipe.CreatedAt)
            .ProjectToLatestRecipeReadModel()
            .Take(count)
            .ToListAsync(cancellationToken);

        return latestRecipes;
    }
}
