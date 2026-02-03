namespace CookBook.IdentityProvider.Application.Users.UseCases.RegisterUser;

public sealed record RegisterUserRequest
{
    public required string DisplayName { get; init; }

    public required string Email { get; init; }

    public required string Password { get; init; }
}
