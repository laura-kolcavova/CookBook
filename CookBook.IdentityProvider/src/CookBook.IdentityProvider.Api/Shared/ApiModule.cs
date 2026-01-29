using Carter;

namespace CookBook.IdentityProvider.Api.Shared;

public abstract class ApiModule : CarterModule
{
    protected ApiModule(string route)
       : base($"/api{route}")
    {
        WithGroupName("Api");
        IncludeInOpenApi();
    }
}
