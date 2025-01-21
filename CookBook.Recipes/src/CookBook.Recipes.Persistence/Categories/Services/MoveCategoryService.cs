using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.Recipes.Application.Categories.Services;
using CookBook.Recipes.Domain.Categories;
using CookBook.Recipes.Persistence.Shared.DatabaseContexts;
using CookBook.Recipes.Persistence.Shared.Exceptions;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Persistence.Categories.Services;

internal sealed class MoveCategoryService(
    RecipesContext recipesContext,
    ILogger<MoveCategoryService> logger) :
    IMoveCategoryService
{
    public async Task<UnitResult<Error>> MoveCategory(
        int id,
        int newParentCategoryId,
        CancellationToken cancellationToken)
    {
        using (logger.BeginScope(new Dictionary<string, object?>
        {
            ["Id"] = id,
            ["NewParentCategoryId"] = newParentCategoryId,
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

                var parentCategory = await recipesContext
                    .Categories
                    .FindAsync(newParentCategoryId, cancellationToken);

                if (parentCategory is null)
                {
                    return Errors.Category.NotFound(newParentCategoryId);
                }

                if (category.Id == parentCategory.Id)
                {
                    return Errors.Category.ParentCategoryIsNotValid(
                        parentCategory.Name,
                        category.Name);
                }

                category.ChangeParentCategory(parentCategory);

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
                    "An unexpected error has occurred while moving a sub category to another parent category");
            }
        }
    }
}
