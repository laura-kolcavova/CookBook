using CookBook.Recipes.Application.Common;
using CookBook.Recipes.Domain.Recipes;

namespace CookBook.Recipes.Application.Features;

public interface IRecipeListingItemsRepository : IReadModelRepository<RecipeListingItemReadModel, long>
{
}
