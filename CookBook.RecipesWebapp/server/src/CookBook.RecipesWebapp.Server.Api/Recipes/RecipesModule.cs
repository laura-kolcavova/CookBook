using CookBook.RecipesWebapp.Server.Api.Shared;

namespace CookBook.RecipesWebapp.Server.Api.Recipes;

public abstract class RecipesModule :
    ApiModule
{
    protected RecipesModule() :
        base("/recipes")
    {
        WithTags("Recipes");
    }
}
