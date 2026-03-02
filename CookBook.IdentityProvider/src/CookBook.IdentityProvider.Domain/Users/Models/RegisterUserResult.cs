using CookBook.IdentityProvider.Domain.Users;

namespace CookBook.IdentityProvider.Domain.Userrs.Models;

public sealed record RegisterUserResult
{
    public required UserAggregate User { get; init; }

    public required CustomIdentityUser IdentityUser { get; init; }
}
