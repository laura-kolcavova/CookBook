using CookBook.IdentityProvider.Domain.Users;

namespace CookBook.IdentityProvider.Domain.Users.Models;

public sealed record RegisterUserResult
{
    public required UserAggregate User { get; init; }

    public required CustomIdentityUser IdentityUser { get; init; }
}
