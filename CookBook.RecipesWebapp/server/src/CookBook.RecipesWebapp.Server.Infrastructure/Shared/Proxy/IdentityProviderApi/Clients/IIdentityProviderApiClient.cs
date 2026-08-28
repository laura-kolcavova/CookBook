using CookBook.RecipesWebapp.Server.Infrastructure.Shared.Proxy.IdentityProviderApi.Dto;
using Refit;

namespace CookBook.RecipesWebapp.Server.Infrastructure.Shared.Proxy.IdentityProviderApi.Clients;

internal interface IIdentityProviderApiClient
{
    [Get("/api/users/{userName}/profile-info")]
    public Task<IApiResponse<GetUserProfileInfoResponseDto>> GetUserProfileInfo(
        string userName,
        CancellationToken cancellationToken);

    [Get("/api/users/current/profile")]
    public Task<IApiResponse<GetCurrentUserProfileResponseDto>> GetCurrentUserProfile(
        [Header("Authorization")] string authorization,
        CancellationToken cancellationToken);
}
