namespace CookBook.RecipesWebapp.Server.Infrastructure.Shared.Proxy.IdentityProviderApi.Dto;

internal sealed record GetCurrentUserProfileResponseDto
{
    public required string DisplayName { get; init; }

    public required string Email { get; init; }
}
