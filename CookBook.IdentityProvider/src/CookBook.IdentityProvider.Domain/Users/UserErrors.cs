using CookBook.Extensions.CSharpExtended.Errors;

namespace CookBook.IdentityProvider.Domain.Users;

public static class UserErrors
{
    public static class User
    {
        public static Error NotFound() => Error.Failure(
            $"{nameof(User)}.{nameof(NotFound)}",
            $"User was not found.");
    }
}
