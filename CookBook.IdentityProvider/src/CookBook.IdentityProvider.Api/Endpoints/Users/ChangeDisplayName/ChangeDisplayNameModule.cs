using CookBook.Extensions.AspNetCore.Abort.Extensions;
using CookBook.Extensions.AspNetCore.Errors.Extensions;
using CookBook.Extensions.AspNetCore.FluentValidation.Extensions;
using CookBook.IdentityProvider.Domain.Users;
using CookBook.IdentityProvider.Domain.Users.Services.Abstractions;
using CookBook.IdentityProvider.Infrastructure.Shared.Configuration;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CookBook.IdentityProvider.Api.Endpoints.Users.ChangeDisplayName;

public sealed class ChangeDisplayNameModule :
    UsersModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapPatch("/current/display-name", HandleAsync)
            .WithName("ChangeDisplayName")
            .WithSummary("Changes the display name of the current user")
            .RequireAuthorization(ConfigurationConstants.AuthenticationPolicies.ReadWrite)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .AddFluentValidation()
            .AddClosedRequest();
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters]
        ChangeDisplayNameParams request,
        ClaimsPrincipal claimsPrincipal,
        UserManager<CustomIdentityUser> userManager,
        IChangeDisplayNameManager changeDisplayNameManager,
        ILogger<ChangeDisplayNameModule> logger,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        try
        {
            var identityUser = await userManager.GetUserAsync(claimsPrincipal);

            if (identityUser is null)
            {
                return TypedResults.Problem(
                    UserErrors
                        .User
                        .NotFound()
                        .AsProblemDetails(httpContext));
            }

            if (request.ChangeDisplayNameRequest.DisplayName == identityUser.UserName)
            {
                return TypedResults.Problem(
                    UserErrors
                        .User
                        .DisplayNameUnchanged()
                        .AsProblemDetails(httpContext));
            }

            await changeDisplayNameManager.ChangeDisplayName(
                identityUser,
                request.ChangeDisplayNameRequest.DisplayName,
                cancellationToken);

            return TypedResults.NoContent();
        }
        catch (Exception ex)
        when (ex is not OperationCanceledException)
        {
            logger.LogError(
                ex,
                "An unexpected error occurred while changing the display name");

            throw;
        }
    }
}
