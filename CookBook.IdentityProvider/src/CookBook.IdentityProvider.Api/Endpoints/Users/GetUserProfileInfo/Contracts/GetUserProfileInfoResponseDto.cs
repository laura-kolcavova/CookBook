namespace CookBook.IdentityProvider.Api.Endpoints.Users.GetUserProfileInfo.Contracts;

public sealed record GetUserProfileInfoResponseDto
{
    public required UserProfileInfoDto UserProfileInfo { get; init; }
}
