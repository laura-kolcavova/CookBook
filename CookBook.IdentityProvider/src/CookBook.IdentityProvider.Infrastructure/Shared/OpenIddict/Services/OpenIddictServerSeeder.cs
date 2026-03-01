using CookBook.IdentityProvider.Infrastructure.Shared.OpenIddict.Services.Abstractions;
using OpenIddict.Abstractions;

namespace CookBook.IdentityProvider.Infrastructure.Shared.OpenIddict.Services;

internal sealed class OpenIddictServerSeeder(
    IOpenIddictApplicationManager openIddictApplicationManager) :
    IOpenIddictServerSeeder
{
    // In real application, these values should be stored in a secure place
    private const string APPLICATION_CLIENT_ID_COOKBOOK_WEBAPP = "CookBook.WebApp";
    private const string APPLICATION_CLIENT_SECRET_COOKBOOK_WEBAPP = "c0741d5c-f119-4b19-be90-08b6bd1084bf";
    private const string APPLICATION_DISPLAY_NAME_COOKBOOK_WEBAPP = "CookBook WebApp";

    public async Task SeedApplications(
        CancellationToken cancellationToken)
    {
        var createCookBookWebapp = await openIddictApplicationManager.FindByClientIdAsync(
            APPLICATION_CLIENT_ID_COOKBOOK_WEBAPP,
            cancellationToken) is null;

        if (createCookBookWebapp)
        {
            var cookBookWebappDescriptor = new OpenIddictApplicationDescriptor
            {
                ClientId = APPLICATION_CLIENT_ID_COOKBOOK_WEBAPP,
                ClientSecret = APPLICATION_CLIENT_SECRET_COOKBOOK_WEBAPP,
                DisplayName = APPLICATION_DISPLAY_NAME_COOKBOOK_WEBAPP,
                ConsentType = OpenIddictConstants.ConsentTypes.Implicit,
                ClientType = OpenIddictConstants.ClientTypes.Public,
                PostLogoutRedirectUris =
                {
                    new Uri("http://localhost:5015/signout-callback-oidc")
                },
                RedirectUris =
                {
                    new Uri("http://localhost:5015/signin-oidc")
                },
                Permissions =
                {
                    OpenIddictConstants.Permissions.Endpoints.Authorization,
                    OpenIddictConstants.Permissions.Endpoints.EndSession,
                    OpenIddictConstants.Permissions.Endpoints.Token,
                    OpenIddictConstants.Permissions.Endpoints.Revocation,
                    OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                    OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                    OpenIddictConstants.Permissions.GrantTypes.Password,
                    OpenIddictConstants.Permissions.ResponseTypes.Code,
                    OpenIddictConstants.Permissions.Scopes.Email,
                    OpenIddictConstants.Permissions.Scopes.Profile,
                    OpenIddictConstants.Permissions.Scopes.Roles
                },
                Requirements =
                {
                    OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange
                }
            };

            await openIddictApplicationManager.CreateAsync(
                cookBookWebappDescriptor,
                cancellationToken);
        }
    }
}
