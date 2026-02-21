using CookBook.Recipes.Domain.Recipes.ReadModels;
using CookBook.Recipes.Domain.Recipes.Services.Abstractions;
using CookBook.Recipes.Infrastructure.Recipes.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Recipes.Infrastructure.Recipes.Services;

internal sealed class GetRecipeDetailQuery(
    RecipesContext recipesContext)
    : IGetRecipeDetailQuery
{
    public async Task<RecipeDetailReadModel?> Execute(
        long recipeId,
        CancellationToken cancellationToken)
    {
        var recipeDetail = await recipesContext
            .Recipes
            .AsNoTracking()
            .Where(
                recipe => recipe.Id == recipeId)
            .ProjectToRecipeDetailReadModel()
            .SingleOrDefaultAsync(
                cancellationToken);

        return recipeDetail;
    }
}
