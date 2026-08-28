namespace CookBook.IdentityProvider.Api.Endpoints.Users.GetCurrentUserProfile.Contracts;

public sealed record GetCurrentUserProfileResponseDto
{
    public required string UserName { get; init; }

    public required string DisplayName { get; init; }

    public required string Email { get; init; }
}
