using Carter;

namespace CookBook.RecipesWebapp.Server.Api.Shared;

public abstract class ApiModule :
    CarterModule
{
    protected ApiModule(string route)
       : base($"/api{route}")
    {
    }
}
