namespace CookBook.IdentityProvider.Api.Users.Endpoints.RegisterUser.Contracts;

internal sealed record RegisterUserRequestDto
{
    public required string DisplayName { get; init; }

    public required string Email { get; init; }

    public required string Password { get; init; }
}
