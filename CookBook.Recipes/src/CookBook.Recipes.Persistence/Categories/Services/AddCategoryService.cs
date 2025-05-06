using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.Recipes.Application.Categories.Models;
using CookBook.Recipes.Application.Categories.Services;
using CookBook.Recipes.Domain.Categories;
using CookBook.Recipes.Persistence.Shared.DatabaseContexts;
using CookBook.Recipes.Persistence.Shared.Exceptions;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Persistence.Categories.Services;

internal sealed class AddCategoryService(
    CategoriesContext categoriesContext,
    ILogger<AddCategoryService> logger) :
    IAddCategoryService
{
    public async Task<Result<AddCategoryResult, Error>> AddCategory(
        string name,
        int parentCategoryId,
        CancellationToken cancellationToken)
    {
        using (logger.BeginScope(new Dictionary<string, object?>
        {
            ["Name"] = name,
            ["ParentCategoryId"] = parentCategoryId,
        }))
        {
            try
            {
                var parentCategory = await categoriesContext
                    .Categories
                    .FindAsync(parentCategoryId, cancellationToken);

                if (parentCategory is null)
                {
                    return Errors.Category.NotFound(parentCategoryId);
                }

                var nameExists = await categoriesContext
                    .Categories
                    .AnyAsync(category =>
                        category.Name == name &&
                        category.ParentCategoryId == parentCategoryId,
                        cancellationToken);

                if (nameExists)
                {
                    return Errors.Category.AnotherCategoryWithNameAlreadyExists(name);
                }

                var newSubCategory = parentCategory is null
                    ? new CategoryAggregate(name)
                    : new CategoryAggregate(name, parentCategory);

                await categoriesContext.AddAsync(newSubCategory, cancellationToken);

                await categoriesContext.SaveChangesAsync(cancellationToken);

                return new AddCategoryResult
                {
                    CategoryId = newSubCategory.Id,
                };
            }
            catch (Exception ex)
            {
                throw RecipesPersistenceException.LogAndCreate(
                    logger,
                    ex,
                    "An unexpected error has occurred while adding a new sub category");
            }
        }
    }

    public Task<Result<AddCategoryResult, Error>> AddCategory(
        string name,
        CancellationToken cancellationToken)
    {
        return AddCategory(
            name,
            CategoryAggregate.RootCategoryId,
            cancellationToken);
    }
}
