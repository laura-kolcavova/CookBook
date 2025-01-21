using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.Recipes.Application.Categories.Services;
using CookBook.Recipes.Domain.Categories;
using CookBook.Recipes.Persistence.Shared.DatabaseContexts;
using CookBook.Recipes.Persistence.Shared.Exceptions;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Persistence.Categories.Services;

internal sealed class RemoveCategoryService(
    RecipesContext recipesContext,
    ILogger<RemoveCategoryService> logger) :
    IRemoveCategoryService
{
    public async Task<UnitResult<Error>> RemoveCategory(
        int id,
        CancellationToken cancellationToken)
    {
        using (logger.BeginScope(new Dictionary<string, object?>
        {
            ["Id"] = id,
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
                await recipesContext
                    .Categories
                    .Where(category => category.ParentCategoryId == id)
                    .ExecuteDeleteAsync(cancellationToken);

                recipesContext
                    .Categories
                    .Remove(category);

                await recipesContext.SaveChangesAsync(cancellationToken);

                return UnitResult.Success<Error>();
            }
            catch (Exception ex)
            {
                throw RecipesPersistenceException.LogAndCreate(
                    logger,
                    ex,
                    "An unexpected error has occurred while removing a category");
            }
        }
    }
}
