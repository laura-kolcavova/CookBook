using System.Security.Claims;

namespace CookBook.Recipes.Api.Shared.Extensions;

internal static class IdpClaimsPrincipalExtensions
{
    private const string ClaimTypeName = "name";

    public static Claim GetUserNameClaim(
        this ClaimsPrincipal idpClaimsPrincipal)
    {
        return idpClaimsPrincipal
            .Claims
            .FirstOrDefault(claim => claim.Type == ClaimTypeName)
            ?? throw new InvalidOperationException("User name is not set.");
    }
}
