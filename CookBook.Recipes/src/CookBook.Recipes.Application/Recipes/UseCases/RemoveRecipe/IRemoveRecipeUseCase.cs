using CookBook.Extensions.CSharpExtended.Errors;
using CSharpFunctionalExtensions;

namespace CookBook.Recipes.Application.Recipes.UseCases.RemoveRecipe;

public interface IRemoveRecipeUseCase
{
    public Task<UnitResult<Error>> RemoveRecipe(
        long recipeId,
        string userName,
        CancellationToken cancellationToken);
}
