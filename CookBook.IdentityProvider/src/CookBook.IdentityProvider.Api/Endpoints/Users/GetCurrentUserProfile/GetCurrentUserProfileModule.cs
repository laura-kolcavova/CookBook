using CookBook.Extensions.AspNetCore.Abort.Extensions;
using CookBook.Extensions.AspNetCore.Errors.Extensions;
using CookBook.IdentityProvider.Api.Endpoints.Users.GetCurrentUserProfile.Contracts;
using CookBook.IdentityProvider.Domain.Users;
using CookBook.IdentityProvider.Domain.Users.Services.Abstractions;
using CookBook.IdentityProvider.Infrastructure.Shared.Configuration;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using System.Security.Claims;

namespace CookBook.IdentityProvider.Api.Endpoints.Users.GetCurrentUserProfile;

public sealed class GetCurrentUserProfileModule :
    UsersModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapGet("/current/profile", HandleAsync)
            .WithName("GetCurrentUserProfile")
            .WithSummary("Gets the profile information of the current user")
            .RequireAuthorization(ConfigurationConstants.AuthenticationPolicies.ReadWrite)
            .Produces<CurrentUserProfileDto>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .AddClosedRequest();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal claimsPrincipal,
        UserManager<CustomIdentityUser> userManager,
        IGetUserProfileInfoQuery getUserProfileInfoQuery,
        HttpContext httpContext,
        ILogger<GetCurrentUserProfileModule> logger,
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

            var userName = await userManager.GetUserNameAsync(identityUser)
                ?? throw new InvalidOperationException("User name is not set.");

            var email = await userManager.GetEmailAsync(identityUser)
                ?? throw new InvalidOperationException("Email is not set.");

            var preferredUsernameClaim = (await userManager.GetClaimsAsync(identityUser))
                .Single(claim => claim.Type == OpenIddictConstants.Claims.PreferredUsername)
                 ?? throw new InvalidOperationException("Preferred user name is not set.");

            var responseDto = new CurrentUserProfileDto
            {
                UserName = userName,
                DisplayName = preferredUsernameClaim.Value,
                Email = email
            };

            return TypedResults.Ok(responseDto);
        }
        catch (Exception ex)
        when (ex is not OperationCanceledException)
        {
            logger.LogError(
                ex,
                "An unexpected error occurred while getting current user profile");

            throw;
        }
    }
}
