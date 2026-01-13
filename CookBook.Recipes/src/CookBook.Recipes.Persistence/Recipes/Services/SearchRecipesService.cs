using CookBook.Recipes.Domain.Recipes.ReadModels;
using CookBook.Recipes.Domain.Recipes.Services.Abstractions;
using CookBook.Recipes.Domain.Shared.Filtering;
using CookBook.Recipes.Domain.Shared.Sorting;
using CookBook.Recipes.Persistence.Recipes.Extensions;
using CookBook.Recipes.Persistence.Shared.Exceptions;
using CookBook.Recipes.Persistence.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Persistence.Recipes.Services;

internal sealed class SearchRecipesService(
    RecipesContext recipesContext,
    ILogger<SearchRecipesService> logger) :
    ISearchRecipesService
{
    public async Task<IReadOnlyCollection<RecipeListingItemReadModel>> SearchRecipes(
        IReadOnlyCollection<SortBy>? sorting,
        OffsetFilter? offsetFilter,
        CancellationToken cancellationToken)
    {
        try
        {
            var queryable = recipesContext.Recipes
                .AsNoTracking();

            if (sorting is not null)
            {
                queryable = queryable
                    .SortBy(sorting);
            }

            if (offsetFilter is not null)
            {
                queryable = queryable
                    .Skip(offsetFilter.Offset)
                    .Take(offsetFilter.Limit);
            }

            return await queryable
               .ProjectToRecipeListingItemReadModel()
               .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw RecipesPersistenceException.LogAndCreate(
                logger,
                ex,
                "An unexpected error occurred while searching for recipes");
        }
    }
}
