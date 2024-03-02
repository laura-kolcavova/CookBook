using CookBook.Recipes.Application.Features;
using CookBook.Recipes.Domain.Recipes;
using CookBook.Recipes.Persistence.Common;
using CookBook.Recipes.Persistence.DatabaseContexts;

namespace CookBook.Recipes.Persistence.Recipes;

internal class RecipeListingItemsRepository :
    EntityFrameworkReadModelRepository<RecipesReadContext, RecipeListingItemReadModel, long>,
    IRecipeListingItemsRepository
{
    public RecipeListingItemsRepository(RecipesReadContext dbContext) : base(dbContext)
    {
    }
}
