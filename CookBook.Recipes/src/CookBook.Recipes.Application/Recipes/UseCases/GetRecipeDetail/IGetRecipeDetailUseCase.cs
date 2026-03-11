using CookBook.Recipes.Domain.Recipes.ReadModels;
using CSharpFunctionalExtensions;

namespace CookBook.Recipes.Application.Recipes.UseCases.GetRecipeDetail;

public interface IGetRecipeDetailUseCase
{
    public Task<Maybe<RecipeDetailReadModel>> GetRecipeDetail(
       long recipeId,
       CancellationToken cancellationToken);
}
