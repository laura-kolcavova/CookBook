using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.Recipes.Application.Categories.Services;
using CookBook.Recipes.Domain.Categories;
using CookBook.Recipes.Persistence.Shared.DatabaseContexts;
using CookBook.Recipes.Persistence.Shared.Exceptions;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Persistence.Categories.Services;

internal sealed class RenameCategoryService(
    RecipesContext recipesContext,
    ILogger<RenameCategoryService> logger) :
    IRenameCategoryService
{
    public async Task<UnitResult<Error>> RenameCategory(
        int id,
        string newName,
        CancellationToken cancellationToken)
    {
        using (logger.BeginScope(new Dictionary<string, object?>
        {
            ["Id"] = id,
            ["NewName"] = newName
        }))
        {
            try
            {
                if (id == CategoryAggregate.RootCategoryId)
                {
                    return Errors.Category.RootCategoryModificationNotAllowed();
                }

                var category = await recipesContext
                    .Categories
                    .FindAsync(id, cancellationToken);

                if (category is null)
                {
                    return Errors.Category.NotFound(id);
                }

                category.Rename(newName);

                recipesContext
                    .Categories
                    .Update(category);

                await recipesContext.SaveChangesAsync(cancellationToken);

                return UnitResult.Success<Error>();
            }
            catch (Exception ex)
            {
                throw RecipesPersistenceException.LogAndCreate(
                    logger,
                    ex,
                    "An unexpected error has occurred while renaming a category");
            }
        }
    }
}
