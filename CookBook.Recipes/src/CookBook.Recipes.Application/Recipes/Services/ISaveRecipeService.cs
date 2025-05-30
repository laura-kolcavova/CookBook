using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.Recipes.Application.Recipes.Models;
using CSharpFunctionalExtensions;

namespace CookBook.Recipes.Application.Recipes.Services;

public interface ISaveRecipeService
{
    public Task<Result<SaveRecipeResultModel, Error>> SaveRecipe(
        SaveRecipeRequestModel request,
        CancellationToken cancellationToken);
}
