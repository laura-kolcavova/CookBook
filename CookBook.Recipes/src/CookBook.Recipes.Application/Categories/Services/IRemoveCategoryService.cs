using CookBook.Extensions.CSharpExtended.Errors;
using CSharpFunctionalExtensions;

namespace CookBook.Recipes.Application.Categories.Services;

public interface IRemoveCategoryService
{
    public Task<UnitResult<Error>> RemoveCategory(
        int id,
        CancellationToken cancellationToken);
}
