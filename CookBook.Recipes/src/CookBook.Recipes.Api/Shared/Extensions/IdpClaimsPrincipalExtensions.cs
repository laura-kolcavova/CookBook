using System.Security.Claims;

namespace CookBook.Recipes.Api.Shared.Extensions;

internal static class IdpClaimsPrincipalExtensions
{
    private const string ClaimTypeName = "name";

    public static string GetUserName(
        this ClaimsPrincipal idpClaimsPrincipal)
    {
        return idpClaimsPrincipal
            .Claims
            .First(claim => claim.Type == ClaimTypeName)
            .Value;
    }
}
