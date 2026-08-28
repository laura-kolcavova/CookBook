namespace CookBook.IdentityProvider.Api.Endpoints.Users.GetUserProfileInfo.Contracts;

public sealed record GetUserProfileInfoResponseDto
{
    public required string DisplayName { get; init; }

    public required string UserName { get; init; }
}
