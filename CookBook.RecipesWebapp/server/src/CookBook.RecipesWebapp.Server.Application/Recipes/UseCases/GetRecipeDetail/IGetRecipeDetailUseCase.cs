using CookBook.RecipesWebapp.Server.Domain.Recipes.Models;
using CSharpFunctionalExtensions;

namespace CookBook.RecipesWebapp.Server.Application.Recipes.UseCases.GetRecipeDetail;

public interface IGetRecipeDetailUseCase
{
    public Task<Maybe<RecipeDetailModel>> GetRecipeDetail(
        long recipeId,
        CancellationToken cancellationToken);
}
