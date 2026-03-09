using CookBook.IdentityProvider.Api.Shared.Extensions;
using CookBook.IdentityProvider.Domain.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System.Security.Claims;

namespace CookBook.IdentityProvider.Api.Endpoints.Authorization.GetUserInfo;

public sealed class GetUserInfoEndpointModule :
    AuthorizationModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapGet("/userinfo", HandleAsync)
            .WithName("GetUserInfo")
            .WithSummary("OpenID Connect userinfo endpoint")
            .WithDescription("")
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .DisableAntiforgery()
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
        var user = await userManager.GetUserAsync(claimsPrincipal);

        if (user is null)
        {
            var properties = new AuthenticationProperties(new Dictionary<string, string?>
            {
                [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant,
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
            [OpenIddictConstants.Claims.Subject] = await userManager.GetUserIdAsync(user)
        };

        if (claimsPrincipal.HasScope(OpenIddictConstants.Scopes.Profile))
        {
            claims[OpenIddictConstants.Claims.Name] = await userManager.GetUserNameAsync(user)
                ?? throw new InvalidOperationException("User name is not set.");

            claims[OpenIddictConstants.Claims.PreferredUsername] = (await userManager.GetClaimsAsync(user))
                .FirstOrDefault(claim => claim.Type == OpenIddictConstants.Claims.PreferredUsername)
                ?.Value
                ?? throw new InvalidOperationException("Preferred user name is not set.");
        }

        if (claimsPrincipal.HasScope(OpenIddictConstants.Scopes.Email))
        {
            claims[OpenIddictConstants.Claims.Email] = await userManager.GetEmailAsync(user)
                ?? throw new InvalidOperationException("Email is not set.");
        }

        return TypedResults.Ok(
            claims);
    }
}
