using CookBook.RecipesWebapp.Server.Api.Shared.Extensions;
using CookBook.RecipesWebapp.Server.Api.Users.Endpoints.LogOut;
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
            .MapGet("/logout", HandleAsync)
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
        [AsParameters] LogOutEndpointParams request,
        CancellationToken cancellationToken)
    {
        var authProperties = new AuthenticationProperties
        {
            RedirectUri = BuildReturnUrl(request.ReturnUrl),
        };

        return TypedResults.SignOut(
            properties: authProperties,
            authenticationSchemes: [
                CookieAuthenticationDefaults.AuthenticationScheme,
                OpenIdConnectDefaults.AuthenticationScheme
            ]);
    }

    private static string BuildReturnUrl(
       string? returnUrl)
    {
        const string pathBase = "/";

        if (string.IsNullOrEmpty(returnUrl))
        {
            return pathBase;
        }

        if (!Uri.IsWellFormedUriString(returnUrl, UriKind.Relative))
        {
            var uri = new Uri(
                returnUrl,
                UriKind.Absolute);

            return uri.PathAndQuery;
        }

        if (returnUrl[0] != '/')
        {
            return $"{pathBase}{returnUrl}";
        }

        return returnUrl;
    }
}
