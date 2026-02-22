using CookBook.Extensions.CSharpExtended.Errors;

namespace CookBook.RecipesWebapp.Server.Domain.Users;

public static class UserErrors
{
    public static class User
    {
        public static Error InvalidCredentials() =>
            Error.Failure(
               $"{nameof(User)}.{nameof(InvalidCredentials)}",
               "The provided email or password is incorrect.");
    }
}
