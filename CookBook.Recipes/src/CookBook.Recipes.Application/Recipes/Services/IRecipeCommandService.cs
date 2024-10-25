using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.Recipes.Application.Recipes.Models;
using CSharpFunctionalExtensions;

namespace CookBook.Recipes.Application.Recipes.Services;

public interface IRecipeCommandService
{
    public Task<UnitResult<Error>> RemoveRecipeAsync(
        long recipeId,
        CancellationToken cancellationToken);

    public Task<Result<SaveRecipeResult, Error>> SaveRecipeAsync(
        SaveRecipeRequest request,
        CancellationToken cancellationToken);
}
