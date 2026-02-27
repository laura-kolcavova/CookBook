using System.Security.Claims;

namespace CookBook.RecipesWebapp.Server.Domain.Users.Models;

public sealed record AuthenticationResult
{
    public required string AccessToken { get; init; }

    public required ClaimsPrincipal IdentityTokenPrincipal { get; init; }

    public required ClaimsPrincipal UserInfoTokenPrincipal { get; init; }

}
