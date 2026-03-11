using CookBook.RecipesWebapp.Server.Api.Shared;

namespace CookBook.RecipesWebapp.Server.Api.Antiforgery;

public abstract class AntiforgeryModule :
    ApiModule
{
    protected AntiforgeryModule()
       : base("/antiforgery")
    {
        WithTags("Antiforgery");
    }
}
