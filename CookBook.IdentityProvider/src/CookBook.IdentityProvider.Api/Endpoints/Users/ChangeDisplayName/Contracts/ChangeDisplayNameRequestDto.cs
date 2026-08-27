namespace CookBook.IdentityProvider.Api.Endpoints.Users.ChangeDisplayName.Contracts;

internal sealed record ChangeDisplayNameRequestDto
{
    public required string DisplayName { get; init; }
}
