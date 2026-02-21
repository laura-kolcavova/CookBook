using Carter;

namespace CookBook.RecipesWebapp.Server.Api.Shared;

public abstract class ApiModule :
    CarterModule
{
    protected ApiModule(string route)
       : base($"/api{route}")
    {
        VerifyUrlPathIsSane(route);

        IncludeInOpenApi();
    }

    private static void VerifyUrlPathIsSane(string relativePath)
    {
        if (string.IsNullOrEmpty(relativePath))
        {
            return;
        }

        if (!relativePath.StartsWith("/"))
        {
            throw new ArgumentException(
                $"URL path {relativePath} must start with '/' and be of the form '/foo/bar/baz'"
            );
        }
    }
}
