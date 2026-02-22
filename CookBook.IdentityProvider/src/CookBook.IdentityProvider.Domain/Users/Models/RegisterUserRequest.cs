namespace CookBook.IdentityProvider.Domain.Users.Models;

public sealed record RegisterUserRequest
{
    public required string DisplayName { get; init; }

    public required string Email { get; init; }

    public required string Password { get; init; }
}
