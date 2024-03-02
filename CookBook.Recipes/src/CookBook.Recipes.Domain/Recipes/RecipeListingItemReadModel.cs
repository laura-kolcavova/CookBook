using CookBook.Recipes.Domain.Shared;

namespace CookBook.Recipes.Domain.Recipes;

public class RecipeListingItemReadModel : ReadModel<long>
{
    public string Title { get; private set; }

    public RecipeListingItemReadModel()
    {
        Title = string.Empty;
    }
}
