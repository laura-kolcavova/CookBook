using CookBook.Recipes.Application.Common.Filtering;
using CookBook.Recipes.Application.Common.Sorting;
using CookBook.Recipes.Domain.Recipes.ReadModels;

namespace CookBook.Recipes.Application.Recipes.Services;

public interface ISearchRecipesService
{
    public Task<IReadOnlyCollection<RecipeListingItemReadModel>> SearchRecipes(
        IReadOnlyCollection<SortBy>? sorting,
        OffsetFilter? offsetFilter,
        CancellationToken cancellationToken);
}
