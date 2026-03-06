using CookBook.RecipesWebapp.Server.Api.Shared.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
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
            .Produces(StatusCodes.Status302Found)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .HandleOperationCancelled()
            .RequireAuthorization(new AuthorizeAttribute
            {
                AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme
            });
    }

    private static IResult HandleAsync(
        CancellationToken cancellationToken)
    {
        var authProperties = new AuthenticationProperties
        {
            RedirectUri = "/"
        };

        return TypedResults.SignOut(
            properties: authProperties,
            authenticationSchemes: [
                CookieAuthenticationDefaults.AuthenticationScheme,
                OpenIdConnectDefaults.AuthenticationScheme
            ]);
    }
}
