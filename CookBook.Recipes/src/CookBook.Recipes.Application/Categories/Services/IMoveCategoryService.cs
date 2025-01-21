using CookBook.Extensions.CSharpExtended.Errors;
using CSharpFunctionalExtensions;

namespace CookBook.Recipes.Application.Categories.Services;

public interface IMoveCategoryService
{
    public Task<UnitResult<Error>> MoveCategory(
        int id,
        int newParentCategoryId,
        CancellationToken cancellationToken);
}
