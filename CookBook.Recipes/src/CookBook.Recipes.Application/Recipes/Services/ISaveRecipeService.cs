using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.Recipes.Application.Recipes.Models;
using CSharpFunctionalExtensions;

namespace CookBook.Recipes.Application.Recipes.Services;

public interface ISaveRecipeService
{
    public Task<Result<SaveRecipeResult, Error>> SaveRecipe(
        SaveRecipeRequest request,
        CancellationToken cancellationToken);
}
