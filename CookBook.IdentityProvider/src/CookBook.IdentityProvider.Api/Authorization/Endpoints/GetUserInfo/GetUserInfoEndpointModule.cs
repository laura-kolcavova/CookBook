using CookBook.IdentityProvider.Api.Shared.Extensions;
using CookBook.IdentityProvider.Domain.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System.Security.Claims;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace CookBook.IdentityProvider.Api.Authorization.Endpoints.GetUserInfo;

public sealed class GetUserInfoEndpointModule :
    AuthorizationModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapGet("/userinfo", HandleAsync)
            .WithName("GetUserInfo")
            .WithSummary("OpenIddict userinfo endpoint")
            .WithDescription("")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem()
            .DisableAntiforgery()
            .ValidateRequest()
            .HandleOperationCancelled()
            .RequireAuthorization(new AuthorizeAttribute
            {
                AuthenticationSchemes = OpenIddictServerAspNetCoreDefaults.AuthenticationScheme
            });
    }

    private static async Task<IResult> HandleAsync(
        [FromServices] UserManager<CustomIdentityUser> userManager,
        ClaimsPrincipal claimsPrincipal,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var userId = claimsPrincipal.GetClaim(Claims.Subject)
            ?? throw new InvalidOperationException("The mandatory sub claim is missing.");

        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
        {
            var properties = new AuthenticationProperties(new Dictionary<string, string?>
            {
                [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
                [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
                   "The username/password couple is invalid."
            });

            return TypedResults.Forbid(
                properties,
                [
                    OpenIddictServerAspNetCoreDefaults.AuthenticationScheme
                ]);
        }

        var claims = new Dictionary<string, object>(StringComparer.Ordinal)
        {
            // Note: the "sub" claim is a mandatory claim and must be included in the JSON response.
            [Claims.Subject] = await userManager.GetUserIdAsync(user)
        };

        if (claimsPrincipal.HasScope(Scopes.Profile))
        {
            claims[Claims.Name] = await userManager.GetUserNameAsync(user)
                ?? throw new InvalidOperationException("User name is not set.");

            claims[Claims.PreferredUsername] = (await userManager.GetClaimsAsync(user))
                .FirstOrDefault(claim => claim.Type == Claims.PreferredUsername)
                ?.Value
                ?? throw new InvalidOperationException("Preferred user name is not set.");
        }

        if (claimsPrincipal.HasScope(Scopes.Email))
        {
            claims[Claims.Email] = await userManager.GetEmailAsync(user)
                ?? throw new InvalidOperationException("Email is not set.");
        }

        return TypedResults.Ok(
            claims);
    }
}
