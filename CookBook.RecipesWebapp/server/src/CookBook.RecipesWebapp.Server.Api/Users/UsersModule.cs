using CookBook.RecipesWebapp.Server.Api.Shared;

namespace CookBook.RecipesWebapp.Server.Api.Users;

public abstract class UsersModule :
    ApiModule
{
    protected UsersModule()
        : base("/users")
    {
        WithTags("Users");
    }
}
