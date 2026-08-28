using OpenIddict.Abstractions;
using System.Security.Claims;

namespace CookBook.IdentityProvider.Api.Shared.Extensions;

internal static class ClaimExtensions
{
    public static IEnumerable<string> GetDestinations(
        this Claim claim)
    {
        switch (claim.Type)
        {
            case OpenIddictConstants.Claims.Name:
                {
                    yield return OpenIddictConstants.Destinations.AccessToken;

                    if (claim.Subject!.HasScope(OpenIddictConstants.Scopes.Profile))
                    {
                        yield return OpenIddictConstants.Destinations.IdentityToken;
                    }

                    yield break;
                }
            case OpenIddictConstants.Claims.PreferredUsername:
                {
                    // Display name is mutable, so it must not be cached in the long-lived access token - it is fetched live from the Identity Provider instead
                    if (claim.Subject!.HasScope(OpenIddictConstants.Scopes.Profile))
                    {
                        yield return OpenIddictConstants.Destinations.IdentityToken;
                    }

                    yield break;
                }
            case OpenIddictConstants.Claims.Email:
                {
                    yield return OpenIddictConstants.Destinations.AccessToken;

                    if (claim.Subject!.HasScope(OpenIddictConstants.Scopes.Email))
                        yield return OpenIddictConstants.Destinations.IdentityToken;

                    yield break;
                }
            case OpenIddictConstants.Claims.Role:
                {
                    yield return OpenIddictConstants.Destinations.AccessToken;

                    if (claim.Subject!.HasScope(OpenIddictConstants.Scopes.Roles))
                    {
                        yield return OpenIddictConstants.Destinations.IdentityToken;
                    }

                    yield break;
                }
            // Never include the security stamp in the access and identity tokens, as it's a secret value.
            case "AspNet.Identity.SecurityStamp":
                yield break;

            default:
                {
                    yield return OpenIddictConstants.Destinations.AccessToken;

                    yield break;
                }
        }
    }
}
