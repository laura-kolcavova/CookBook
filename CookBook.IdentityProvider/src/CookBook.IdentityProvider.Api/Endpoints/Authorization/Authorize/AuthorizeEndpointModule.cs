using CookBook.IdentityProvider.Api.Shared.Extensions;
using CookBook.IdentityProvider.Domain.Users;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System.Collections.Immutable;
using System.Security.Claims;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace CookBook.IdentityProvider.Api.Endpoints.Authorization.Authorize;

public sealed class AuthorizeEndpointModule :
        AuthorizationModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapMethods(
                "/authorize",
                [HttpMethods.Get, HttpMethods.Post],
                HandleAsync)
            .WithName("Authorize")
            .WithSummary("OpenID Connect authorization endpoint")
            .WithDescription("")
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem()
            .HandleOperationCancelled()
            .AllowAnonymous();
    }

    private static async Task<IResult> HandleAsync(
       [FromServices] UserManager<CustomIdentityUser> userManager,
       [FromServices] IOpenIddictApplicationManager applicationManager,
       [FromServices] IOpenIddictAuthorizationManager authorizationManager,
       [FromServices] IOpenIddictScopeManager scopeManager,
       HttpContext httpContext,
       CancellationToken cancellationToken)
    {
        var openIddictRequest = httpContext.GetOpenIddictServerRequest()
            ?? throw new InvalidOperationException(
            "The OpenID Connect request cannot be retrieved.");

        var httpRequest = httpContext.Request;

        if (openIddictRequest.HasPromptValue(PromptValues.Login))
        {
            var prompt = string.Join(
                " ",
                openIddictRequest
                    .GetPromptValues()
                    .Remove(PromptValues.Login));

            var parameters = httpRequest
                .HasFormContentType
                ? httpRequest
                    .Form
                    .Where(parameter => parameter.Key != Parameters.Prompt)
                    .ToList()
                : httpRequest
                    .Query
                    .Where(parameter => parameter.Key != Parameters.Prompt)
                    .ToList();

            parameters.Add(
                KeyValuePair.Create(
                    Parameters.Prompt,
                    new StringValues(prompt)));

            var returnUrl = httpRequest.PathBase
                + httpRequest.Path
                + QueryString.Create(parameters);

            return TypedResults.Challenge(
                properties: new AuthenticationProperties
                {
                    RedirectUri = returnUrl
                },
                authenticationSchemes: [
                    IdentityConstants.ApplicationScheme
                ]);
        }

        var authenticationResult = await httpContext.AuthenticateAsync();

        if (
            authenticationResult is not { Succeeded: true } ||

                openIddictRequest.MaxAge is not null &&
                authenticationResult.Properties?.IssuedUtc is not null &&
                DateTimeOffset.UtcNow - authenticationResult.Properties.IssuedUtc > TimeSpan.FromSeconds(openIddictRequest.MaxAge.Value)

        )
        {
            if (openIddictRequest.HasPromptValue(PromptValues.None))
            {
                return TypedResults.Forbid(
                    properties: new AuthenticationProperties(new Dictionary<string, string?>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.LoginRequired,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The user is not logged in."
                    }),
                    authenticationSchemes: [
                        OpenIddictServerAspNetCoreDefaults.AuthenticationScheme
                    ]);
            }

            var redirectUrl = httpRequest.PathBase
              + httpRequest.Path
              + QueryString.Create(
                  httpRequest.HasFormContentType
                  ? httpRequest.Form.ToList()
                  : httpRequest.Query.ToList());

            return TypedResults.Challenge(
                properties: new AuthenticationProperties
                {
                    RedirectUri = redirectUrl
                },
                authenticationSchemes: [
                    IdentityConstants.ApplicationScheme
                ]);
        }

        var user = await userManager.GetUserAsync(
            authenticationResult.Principal)
            ?? throw new InvalidOperationException("The user details cannot be retrieved.");

        var application = await applicationManager.FindByClientIdAsync(
            openIddictRequest.ClientId!)
            ?? throw new InvalidOperationException("Details concerning the calling client application cannot be found.");

        var authorizations = await authorizationManager
            .FindAsync(
                subject: await userManager.GetUserIdAsync(user),
                client: await applicationManager.GetIdAsync(application),
                status: Statuses.Valid,
                type: AuthorizationTypes.Permanent,
                scopes: openIddictRequest.GetScopes())
            .ToListAsync(cancellationToken);

        var consentType = await applicationManager.GetConsentTypeAsync(application);

        switch (consentType)
        {
            case ConsentTypes.External
                when authorizations.Count is 0:
                {
                    return TypedResults.Forbid(
                        properties: new AuthenticationProperties(new Dictionary<string, string?>
                        {
                            [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.ConsentRequired,
                            [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
                                "The logged in user is not allowed to access this client application."
                        }),
                        authenticationSchemes: [
                            OpenIddictServerAspNetCoreDefaults.AuthenticationScheme
                        ]
                       );
                }
            case ConsentTypes.Implicit:
            case ConsentTypes.External:
            //when authorizations.Count is not 0:
            case ConsentTypes.Explicit
                when !openIddictRequest.HasPromptValue(PromptValues.Consent):
                //when authorizations.Count is not 0 &&
                //!openIddictRequest.HasPromptValue(PromptValues.Consent):
                {
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

                    identity.SetScopes(scopes);

                    var resources = await scopeManager
                        .ListResourcesAsync(scopes, cancellationToken)
                        .ToListAsync(cancellationToken);

                    var authorization = authorizations.LastOrDefault();

                    authorization ??= await authorizationManager.CreateAsync(
                        identity: identity,
                        subject: await userManager.GetUserIdAsync(user),
                        client: (await applicationManager.GetIdAsync(application))!,
                        type: AuthorizationTypes.Permanent,
                        scopes: identity.GetScopes());

                    identity.SetAuthorizationId(
                        await authorizationManager.GetIdAsync(authorization));

                    identity.SetDestinations(ClaimExtensions.GetDestinations);

                    return TypedResults.SignIn(
                        new ClaimsPrincipal(identity),
                        authenticationScheme: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
                }
            case ConsentTypes.Explicit
                when openIddictRequest.HasPromptValue(PromptValues.None):
            case ConsentTypes.Systematic
                when openIddictRequest.HasPromptValue(PromptValues.None):
                {
                    return TypedResults.Forbid(
                        properties: new AuthenticationProperties(new Dictionary<string, string?>
                        {
                            [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.ConsentRequired,
                            [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
                                "Interactive user consent is required."
                        }),
                        authenticationSchemes: [
                            OpenIddictServerAspNetCoreDefaults.AuthenticationScheme
                        ]);
                }

            default:
                {
                    throw new Exception("");

                    //return View(new AuthorizeViewModel
                    //{
                    //    ApplicationName = await _applicationManager.GetLocalizedDisplayNameAsync(application),
                    //    Scope = request.Scope
                    //});
                }
        }
    }
}
