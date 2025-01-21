using CookBook.Extensions.CSharpExtended.Errors;
using CSharpFunctionalExtensions;

namespace CookBook.Recipes.Application.Categories.Services;

public interface IRenameCategoryService
{
    public Task<UnitResult<Error>> RenameCategory(
       int id,
       string newName,
       CancellationToken cancellationToken);
}
