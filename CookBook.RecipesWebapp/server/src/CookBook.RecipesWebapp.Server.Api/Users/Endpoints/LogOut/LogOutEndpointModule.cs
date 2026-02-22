using CookBook.RecipesWebapp.Server.Api.Shared.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace CookBook.RecipesWebapp.Server.Api.Users.Endpoints.LogIn;

public sealed class LogOutEndpointModule :
    UsersModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapPost("/logout", HandleAsync)
            .WithName("LogOut")
            .WithSummary("Signs out an user")
            .WithDescription("")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .HandleOperationCancelled()
            .RequireAuthorization(new AuthorizeAttribute
            {
                AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme
            });
    }

    private static async Task<IResult> HandleAsync(
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var authProperties = new AuthenticationProperties
        {
        };

        await httpContext.SignOutAsync(
            scheme: CookieAuthenticationDefaults.AuthenticationScheme,
            properties: authProperties);

        return TypedResults.NoContent();
    }
}
