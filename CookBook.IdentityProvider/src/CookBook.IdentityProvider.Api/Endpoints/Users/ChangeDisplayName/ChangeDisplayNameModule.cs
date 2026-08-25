using CookBook.IdentityProvider.Api.Endpoints.Users.ChangeDisplayName.Contracts;
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
        var displayName = request.ChangeDisplayNameRequest.DisplayName;

        if (displayName.Length > 256)
        {
            return TypedResults.ValidationProblem(new Dictionary<string, string[]>
            {
                [nameof(ChangeDisplayNameRequestDto.DisplayName)] =
                    ["The display name must be less than 256 characters."]
            });
        }

        var identityUserId = userManager.GetUserId(claimsPrincipal)
            ?? throw new InvalidOperationException("Authenticated user must have a subject claim.");

        try
        {
            await changeDisplayNameManager.ChangeDisplayName(
                int.Parse(identityUserId),
                displayName,
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
