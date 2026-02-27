using CookBook.IdentityProvider.Api.Shared.Extensions;
using CookBook.IdentityProvider.Domain.Users;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System.Collections.Immutable;
using System.Security.Claims;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace CookBook.IdentityProvider.Api.Authorization.Endpoints.Token;

public sealed class TokenEndpointModule :
    AuthorizationModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapPost("/token", HandleAsync)
            .WithName("Token")
            .WithSummary("OpenIddict token endpoint")
            .WithDescription("")
            .Accepts<IFormCollection>("application/x-www-form-urlencoded")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem()
            .ValidateRequest()
            .HandleOperationCancelled()
            .AllowAnonymous();
    }

    private static Task<IResult> HandleAsync(
        [FromServices] UserManager<CustomIdentityUser> userManager,
        [FromServices] SignInManager<CustomIdentityUser> signInManager,
        [FromServices] IOpenIddictScopeManager scopeManager,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var openIddictRequest = httpContext.GetOpenIddictServerRequest()
            ?? throw new InvalidOperationException(
                "The OpenID Connect request cannot be retrieved.");

        if (openIddictRequest.IsPasswordGrantType())
        {
            return HandlePasswordGrantTypeAsync(
                openIddictRequest,
                userManager,
                signInManager,
                scopeManager,
                httpContext,
                cancellationToken);
        }

        throw new NotImplementedException(
            "The specified grant type is not implemented.");
    }

    private static async Task<IResult> HandlePasswordGrantTypeAsync(
        OpenIddictRequest openIddictRequest,
        UserManager<CustomIdentityUser> userManager,
        SignInManager<CustomIdentityUser> signInManager,
        IOpenIddictScopeManager scopeManager,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(
            openIddictRequest.Username!);

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
        var signInResult = await signInManager.CheckPasswordSignInAsync(
            user,
            openIddictRequest.Password!,
            lockoutOnFailure: true);

        if (!signInResult.Succeeded)
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

        var identity = new ClaimsIdentity(
            authenticationType: TokenValidationParameters.DefaultAuthenticationType,
            nameType: Claims.Name,
            roleType: Claims.Role);

        var preferredUsernameClaimValue = (await userManager.GetClaimsAsync(user))
                .FirstOrDefault(claim => claim.Type == Claims.PreferredUsername)
                ?.Value
                ?? throw new InvalidOperationException("Preferred user name is not set.");

        identity
            .SetClaim(Claims.Subject, await userManager.GetUserIdAsync(user))
            .SetClaim(Claims.Email, await userManager.GetEmailAsync(user))
            .SetClaim(Claims.Name, await userManager.GetUserNameAsync(user))
            .SetClaim(Claims.PreferredUsername, preferredUsernameClaimValue)
            .SetClaims(Claims.Role, (await userManager.GetRolesAsync(user)).ToImmutableArray());

        var restrictedScopes = new string[]
        {
            Scopes.OpenId,
            Scopes.Email,
            Scopes.Profile,
        };

        var requestScopes = openIddictRequest.GetScopes();

        var scopes = requestScopes
            .Intersect(requestScopes)
            .ToImmutableArray();

        identity
            .SetScopes(scopes);

        var resources = await scopeManager
            .ListResourcesAsync(scopes, cancellationToken)
            .ToListAsync(cancellationToken);

        identity.SetResources(resources);

        identity.SetDestinations(GetDestinations);

        return TypedResults.SignIn(
            new ClaimsPrincipal(identity),
            authenticationScheme: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }

    private static IEnumerable<string> GetDestinations(
        Claim claim)
    {
        // Note: by default, claims are NOT automatically included in the access and identity tokens.
        // To allow OpenIddict to serialize them, you must attach them a destination, that specifies
        // whether they should be included in access tokens, in identity tokens or in both.
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