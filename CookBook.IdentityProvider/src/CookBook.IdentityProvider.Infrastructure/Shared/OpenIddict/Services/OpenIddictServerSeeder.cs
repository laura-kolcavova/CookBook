using CookBook.IdentityProvider.Infrastructure.Shared.OpenIddict.Services.Abstractions;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

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
                Permissions =
                {
                    Permissions.Endpoints.Authorization,
                    Permissions.Endpoints.Token,
                    Permissions.GrantTypes.AuthorizationCode,
                    Permissions.GrantTypes.RefreshToken,
                    Permissions.GrantTypes.Password,
                    Permissions.ResponseTypes.Code,
                    Permissions.Scopes.Email,
                    Permissions.Scopes.Profile,
                    Permissions.Scopes.Roles
                },
                Requirements =
                {
                    Requirements.Features.ProofKeyForCodeExchange
                }
            };

            await openIddictApplicationManager.CreateAsync(
                cookBookWebappDescriptor,
                cancellationToken);
        }
    }
}
