using CookBook.Recipes.Domain.Recipes.ReadModels;
using CookBook.Recipes.Domain.Shared.Filtering;
using CookBook.Recipes.Domain.Shared.Sorting;

namespace CookBook.Recipes.Domain.Recipes.Services;

public interface ISearchRecipesService
{
    public Task<IReadOnlyCollection<RecipeListingItemReadModel>> SearchRecipes(
        IReadOnlyCollection<SortBy>? sorting,
        OffsetFilter? offsetFilter,
        CancellationToken cancellationToken);
}
