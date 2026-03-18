using CookBook.IdentityProvider.Api.Shared;

namespace CookBook.IdentityProvider.Api.Endpoints.Users;

public abstract class UsersModule :
    ApiModule
{
    protected UsersModule()
      : base("/users")
    {
        WithTags("Users");
    }
}
