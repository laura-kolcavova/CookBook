using CookBook.Extensions.AspNetCore.Abort.Extensions;
using CookBook.RecipesWebapp.Server.Api.Users.Endpoints.GetCurrentUser.Contracts;
using CookBook.RecipesWebapp.Server.Domain.Users.Services.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using OpenIddict.Abstractions;
using System.Security.Claims;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace CookBook.RecipesWebapp.Server.Api.Users.Endpoints.GetCurrentUser;

public sealed class GetCurrentUserEndpointModule :
    UsersModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapGet("/current", HandleAsync)
            .WithName("GetCurrentUser")
            .WithSummary("Gets current user info")
            .Produces(StatusCodes.Status200OK, typeof(CurrentUserDto))
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .AddClosedRequest();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal? claimsPrincipal,
        HttpContext httpContext,
        ICurrentUserProfileFetcher currentUserProfileFetcher,
        CancellationToken cancellationToken)
    {
        var isAuthenticated = claimsPrincipal
            ?.Identity
            ?.IsAuthenticated
            ?? false;

        if (!isAuthenticated)
        {
            return TypedResults.Ok(
                CurrentUserDto.Anonymous);
        }

        var accessToken = await httpContext
            .GetTokenAsync(OpenIdConnectParameterNames.AccessToken)
            ?? throw new InvalidOperationException("Authenticated user must have an access token.");

        var currentUserProfile = await currentUserProfileFetcher.FetchCurrentUserProfile(
            accessToken,
            cancellationToken);

        var currentUserDto = new CurrentUserDto
        {
            IsAuthenticated = true,
            UserName = claimsPrincipal!
                .GetClaim(Claims.Name)
                ?? throw new InvalidOperationException("Authenticated user must have a name claim."),
            DisplayName = currentUserProfile.DisplayName,
            Email = currentUserProfile.Email
        };

        return TypedResults.Ok(
            currentUserDto);
    }
}
