namespace CookBook.IdentityProvider.Api.Endpoints.Users.GetUserProfileInfo.Contracts;

public sealed record UserProfileInfoDto
{
    public required string DisplayName { get; init; }

    public required string UserName { get; init; }
}
