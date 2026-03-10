using CookBook.Extensions.AspNetCore.Abort.Extensions;
using CookBook.RecipesWebapp.Server.Api.Users.Endpoints.GetCurrentUser.Contracts;
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
            .MapGet("/current", Handle)
            .WithName("GetCurrentUser")
            .WithSummary("Gets current user info")
            .WithDescription("")
            .Produces(StatusCodes.Status200OK, typeof(CurrentUserDto))
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .AddClosedRequest()
            .AllowAnonymous();
    }

    private static IResult Handle(
        ClaimsPrincipal? claimsPrincipal)
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

        var currentUserDto = new CurrentUserDto
        {
            IsAuthenticated = true,
            UserName = claimsPrincipal!
                .GetClaim(Claims.Name)
                ?? throw new InvalidOperationException("Authenticated user must have a name claim."),
            DisplayName = claimsPrincipal!
                .GetClaim(Claims.PreferredUsername)
                ?? throw new InvalidOperationException("Authenticated user must have a preferred_username claim."),
            Email = claimsPrincipal!
                .GetClaim(Claims.Email)
                ?? throw new InvalidOperationException("Authenticated user must have an email claim.")
        };

        return TypedResults.Ok(
            currentUserDto);
    }
}
