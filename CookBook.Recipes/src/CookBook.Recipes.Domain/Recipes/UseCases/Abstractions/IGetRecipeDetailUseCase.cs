using CookBook.Recipes.Domain.Recipes.ReadModels;
using CSharpFunctionalExtensions;

namespace CookBook.Recipes.Domain.Recipes.UseCases.Abstractions;

public interface IGetRecipeDetailUseCase
{
    public Task<Maybe<RecipeDetailReadModel>> GetRecipeDetail(
       long recipeId,
       CancellationToken cancellationToken);
}
