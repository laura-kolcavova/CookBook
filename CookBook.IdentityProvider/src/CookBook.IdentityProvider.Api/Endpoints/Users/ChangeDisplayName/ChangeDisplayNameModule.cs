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
            .WithDescription("")
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .DisableAntiforgery()
            .RequireAuthorization(ConfigurationConstants.AuthenticationPolicies.ReadWrite);
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters]
        ChangeDisplayNameParams request,
        ClaimsPrincipal claimsPrincipal,
        UserManager<CustomIdentityUser> userManager,
        IChangeDisplayNameManager changeDisplayNameManager,
        ILogger<ChangeDisplayNameModule> logger,
        CancellationToken cancellationToken)
    {
        try
        {
            var identityUser = await userManager.GetUserAsync(claimsPrincipal);

            if (identityUser is null)
            {
                throw new InvalidOperationException("Identity user not found.");
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
