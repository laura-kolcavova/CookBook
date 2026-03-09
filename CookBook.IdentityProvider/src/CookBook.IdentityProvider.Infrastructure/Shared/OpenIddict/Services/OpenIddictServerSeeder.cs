using CookBook.IdentityProvider.Infrastructure.Shared.OpenIddict.Services.Abstractions;
using OpenIddict.Abstractions;

namespace CookBook.IdentityProvider.Infrastructure.Shared.OpenIddict.Services;

internal sealed class OpenIddictServerSeeder(
    IOpenIddictApplicationManager openIddictApplicationManager,
    IOpenIddictScopeManager openIddictScopeManager) :
    IOpenIddictServerSeeder
{
    // In real application, these values should be stored in a secure place
    private const string COOKBOOK_RECIPES__APPLICATION_CLIENT_ID = "CookBook.Recipes";
    private const string COOKBOOK_RECIPES__APPLICATION_CLIENT_SECRET = "b309958a-f14c-41ea-9dbb-8fc51d5debe2";

    private const string COOKBOOK_RECIPES_WEBAPP__APPLICATION_CLIENT_ID = "CookBook.RecipesWebApp";
    private const string COOKBOOK_RECIPES_WEBAPP__APPLICATION_CLIENT_SECRET = "c0741d5c-f119-4b19-be90-08b6bd1084bf";
    private const string COOKBOOK_RECIPES_WEBAPP__APPLICATION_DISPLAY_NAME = "CookBook Recipes WebApp";

    private const string COOKBOOK_RECIPES__READ_WRITE__RESOURCE = "CookBook.Recipes.ReadWrite";

    public async Task SeedApplications(
        CancellationToken cancellationToken)
    {
        var createCookBookRecipes = await openIddictApplicationManager.FindByClientIdAsync(
            COOKBOOK_RECIPES__APPLICATION_CLIENT_ID,
            cancellationToken) is null;

        if (createCookBookRecipes)
        {
            var cookBookRecipesDescriptor = new OpenIddictApplicationDescriptor
            {
                ClientId = COOKBOOK_RECIPES__APPLICATION_CLIENT_ID,
                ClientSecret = COOKBOOK_RECIPES__APPLICATION_CLIENT_SECRET,

                Permissions =
                {
                    OpenIddictConstants.Permissions.Endpoints.Introspection
                },
            };

            await openIddictApplicationManager.CreateAsync(
                cookBookRecipesDescriptor,
                cancellationToken);
        }

        var createCookBookRecipesWebapp = await openIddictApplicationManager.FindByClientIdAsync(
            COOKBOOK_RECIPES_WEBAPP__APPLICATION_CLIENT_ID,
            cancellationToken) is null;

        if (createCookBookRecipesWebapp)
        {
            var cookBookRecipesWebappDescriptor = new OpenIddictApplicationDescriptor
            {
                ClientId = COOKBOOK_RECIPES_WEBAPP__APPLICATION_CLIENT_ID,
                ClientSecret = COOKBOOK_RECIPES_WEBAPP__APPLICATION_CLIENT_SECRET,
                DisplayName = COOKBOOK_RECIPES_WEBAPP__APPLICATION_DISPLAY_NAME,
                ConsentType = OpenIddictConstants.ConsentTypes.Implicit,
                ClientType = OpenIddictConstants.ClientTypes.Confidential,
                PostLogoutRedirectUris =
                {
                    new Uri("http://localhost:5015/signout-callback-oidc"),
                    new Uri("http://localhost:8015/signout-callback-oidc")
                },
                RedirectUris =
                {
                    new Uri("http://localhost:5015/signin-oidc"),
                    new Uri("http://localhost:8015/signin-oidc")
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
                    OpenIddictConstants.Permissions.Scopes.Roles,
                    OpenIddictConstants.Permissions.Prefixes.Scope + COOKBOOK_RECIPES__READ_WRITE__RESOURCE
                },
                Requirements =
                {
                    OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange
                }
            };

            await openIddictApplicationManager.CreateAsync(
                cookBookRecipesWebappDescriptor,
                cancellationToken);
        }
    }

    public async Task SeedResources(
        CancellationToken cancellationToken)
    {
        var createCookBookRecipesReadWriteResource = await openIddictScopeManager.FindByNameAsync(
            COOKBOOK_RECIPES__READ_WRITE__RESOURCE,
            cancellationToken) is null;

        if (createCookBookRecipesReadWriteResource)
        {
            var cookBookRecipesReadWriteResourceDescriptor = new OpenIddictScopeDescriptor
            {
                DisplayName = "Recipes API Read&Write permission",
                Name = COOKBOOK_RECIPES__READ_WRITE__RESOURCE,
                Resources =
                {
                    COOKBOOK_RECIPES__APPLICATION_CLIENT_ID
                }
            };

            await openIddictScopeManager.CreateAsync(
                cookBookRecipesReadWriteResourceDescriptor,
                cancellationToken);
        }
    }
}
