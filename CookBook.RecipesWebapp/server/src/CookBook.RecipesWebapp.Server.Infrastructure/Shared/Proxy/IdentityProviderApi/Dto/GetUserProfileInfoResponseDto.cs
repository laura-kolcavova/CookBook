namespace CookBook.RecipesWebapp.Server.Infrastructure.Shared.Proxy.IdentityProviderApi.Dto;

internal class GetUserProfileInfoResponseDto
{
    public required UserProfileInfoDto UserProfileInfo { get; init; }

    public sealed record UserProfileInfoDto
    {
        public required string DisplayName { get; init; }

        public required string UserName { get; init; }
    }
}
