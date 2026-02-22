using CookBook.RecipesWebapp.Server.Api.Shared.Authentication;
using CookBook.RecipesWebapp.Server.Api.Shared.Extensions;
using CookBook.RecipesWebapp.Server.Api.Users.Endpoints.GetCurrentUser.Contracts;
using System.Security.Claims;

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
            .ProducesValidationProblem()
            .HandleOperationCancelled()

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
            DisplayName = claimsPrincipal!
                .Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Email)
                ?.Value
                ?? string.Empty,
            Email = claimsPrincipal!
                .Claims
                .FirstOrDefault(c => c.Type == AuthenticationConstants.CustomClaimTypes.DisplayName)
                ?.Value
                ?? string.Empty
        };

        return TypedResults.Ok(
            currentUserDto);
    }
}
