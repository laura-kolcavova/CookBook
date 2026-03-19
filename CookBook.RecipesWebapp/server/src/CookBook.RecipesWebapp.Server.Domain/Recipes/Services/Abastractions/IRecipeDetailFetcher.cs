using CookBook.RecipesWebapp.Server.Domain.Recipes.Models;
using CSharpFunctionalExtensions;

namespace CookBook.RecipesWebapp.Server.Domain.Recipes.Services.Abastractions;

public interface IRecipeDetailFetcher
{
    public Task<Maybe<RecipeDetailModel>> FetchRecipeDetail(
        long recipeId,
        CancellationToken cancellationToken);
}
