namespace CookBook.RecipesWebapp.Server.Infrastructure.Shared.Proxy.IdentityProviderApi.Dto;

internal sealed record GetUserProfileInfoResponseDto
{
    public required string DisplayName { get; init; }

    public required string UserName { get; init; }
}
