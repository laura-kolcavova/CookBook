using CookBook.IdentityProvider.Api.Shared;

namespace CookBook.IdentityProvider.Api.Users;

public abstract class UsersModule : ApiModule
{
    protected UsersModule()
        : base("/users")
    {
        WithTags("Users");
    }
}
