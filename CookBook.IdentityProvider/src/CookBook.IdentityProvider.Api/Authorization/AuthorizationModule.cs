using CookBook.IdentityProvider.Api.Shared;

namespace CookBook.IdentityProvider.Api.Authorization;

public abstract class AuthorizationModule :
    ApiModule
{
    protected AuthorizationModule()
       : base("/authorization")
    {
        WithTags("Authorization");
    }
}
