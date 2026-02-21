
using CookBook.RecipesWebapp.Server.Api.Shared.Authentication;
using CookBook.RecipesWebapp.Server.Api.Shared.Extensions;
using CookBook.RecipesWebapp.Server.Application.Users.UseCases.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using OpenIddict.Abstractions;
using System.Security.Claims;

namespace CookBook.RecipesWebapp.Server.Api.Users.Endpoints.LogIn;

public sealed class LogInEndpointModule :
    UsersModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapPost("/login", HandleAsync)
            .WithName("LogIn")
            .WithSummary("Signs in a user")
            .WithDescription("")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem()
            .HandleOperationCancelled()
            .ValidateRequest()
            .AllowAnonymous();
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters] LogInEndpointParams request,
        IAuthenticateUserUseCase logInUseCase,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var logInRequest = request.LogInRequest;

        await logInUseCase.AuthenticateUser(
            logInRequest.Email,
            logInRequest.Password,
            cancellationToken);

        var identity = new ClaimsIdentity(
            authenticationType: CookieAuthenticationDefaults.AuthenticationScheme,
            nameType: ClaimTypes.Name,
            roleType: ClaimTypes.Role);

        // Add the claims that will be persisted in the tokens.
        identity
            .SetClaim(
                ClaimTypes.Email,
                "TestEmail")
            .SetClaim(
                AuthenticationConstants.CustomClaimTypes.DisplayName,
                "TestDisplayName");

        var authProperties = new AuthenticationProperties
        {
        };

        await httpContext.SignInAsync(
           scheme: CookieAuthenticationDefaults.AuthenticationScheme,
           principal: new ClaimsPrincipal(identity),
           properties: authProperties);

        return TypedResults.NoContent();
    }
}
