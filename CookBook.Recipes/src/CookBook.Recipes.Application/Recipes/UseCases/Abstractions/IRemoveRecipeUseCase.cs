using CookBook.Extensions.CSharpExtended.Errors;
using CSharpFunctionalExtensions;

namespace CookBook.Recipes.Application.Recipes.UseCases.Abstractions;

public interface IRemoveRecipeUseCase
{
    public Task<UnitResult<Error>> RemoveRecipe(
        long recipeId,
        string userName,
        CancellationToken cancellationToken);
}
