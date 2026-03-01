using OpenIddict.Abstractions;
using System.Security.Claims;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace CookBook.IdentityProvider.Api.Shared.Extensions;

internal static class ClaimExtensions
{
    public static IEnumerable<string> GetDestinations(
        this Claim claim)
    {
        switch (claim.Type)
        {
            case Claims.Name or Claims.PreferredUsername:
                {
                    yield return Destinations.AccessToken;

                    if (claim.Subject!.HasScope(Scopes.Profile))
                    {
                        yield return Destinations.IdentityToken;
                    }

                    yield break;
                }
            case Claims.Email:
                {
                    yield return Destinations.AccessToken;

                    if (claim.Subject!.HasScope(Scopes.Email))
                        yield return Destinations.IdentityToken;

                    yield break;
                }
            case Claims.Role:
                {
                    yield return Destinations.AccessToken;

                    if (claim.Subject!.HasScope(Scopes.Roles))
                    {
                        yield return Destinations.IdentityToken;
                    }

                    yield break;
                }
            // Never include the security stamp in the access and identity tokens, as it's a secret value.
            case "AspNet.Identity.SecurityStamp":
                yield break;

            default:
                {
                    yield return Destinations.AccessToken;

                    yield break;
                }
        }
    }
}
