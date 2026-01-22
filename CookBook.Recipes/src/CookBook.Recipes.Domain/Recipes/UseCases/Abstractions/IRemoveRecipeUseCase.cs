using CookBook.Extensions.CSharpExtended.Errors;
using CSharpFunctionalExtensions;

namespace CookBook.Recipes.Domain.Recipes.UseCases.Abstractions;

public interface IRemoveRecipeUseCase
{
    public Task<UnitResult<Error>> RemoveRecipe(
    long recipeId,
    int userId,
    CancellationToken cancellationToken);
}
