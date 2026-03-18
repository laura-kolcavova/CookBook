using CookBook.RecipesWebapp.Server.Infrastructure.Shared.Proxy.IdentityProviderApi.Dto;
using Refit;

namespace CookBook.RecipesWebapp.Server.Infrastructure.Shared.Proxy.IdentityProviderApi.Clients;

internal interface IIdentityProviderClient
{
    public Task<IApiResponse<GetUserProfileInfoResponseDto>> GetUserProfileInfo(
        string userName,
        CancellationToken cancellationToken);
}
