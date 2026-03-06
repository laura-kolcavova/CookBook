using Carter;

namespace CookBook.IdentityProvider.Api.Endpoints.Authorization;

public abstract class AuthorizationModule :
    CarterModule
{
    protected AuthorizationModule()
       : base("/connect")
    {
        WithTags("Authorization");
    }
}
