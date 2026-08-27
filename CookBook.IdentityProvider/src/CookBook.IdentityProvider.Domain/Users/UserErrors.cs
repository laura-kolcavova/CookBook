using CookBook.Extensions.CSharpExtended.Errors;

namespace CookBook.IdentityProvider.Domain.Users;

public static class UserErrors
{
    public static class User
    {
        public static Error NotFound() => Error.Failure(
            $"{nameof(User)}.{nameof(NotFound)}",
            $"User was not found.");

        public static Error DisplayNameUnchanged() => Error.Failure(
            $"{nameof(User)}.{nameof(DisplayNameUnchanged)}",
            $"The provided display name is the same as the current display name.");
    }
}
