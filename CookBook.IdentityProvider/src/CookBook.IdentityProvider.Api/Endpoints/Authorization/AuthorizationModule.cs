using CookBook.IdentityProvider.Api.Shared;

namespace CookBook.IdentityProvider.Api.Endpoints.Authorization;

public abstract class AuthorizationModule :
    ApiModule
{
    protected AuthorizationModule()
       : base("/authorization")
    {
        WithTags("Authorization");
    }
}
