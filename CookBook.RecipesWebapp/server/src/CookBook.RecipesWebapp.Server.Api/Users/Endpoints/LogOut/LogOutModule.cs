using CookBook.Extensions.AspNetCore.Abort.Extensions;
using CookBook.RecipesWebapp.Server.Infrastructure.Shared.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace CookBook.RecipesWebapp.Server.Api.Users.Endpoints.LogOut;

public sealed class LogOutModule :
    UsersModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapGet("/logout", HandleAsync)
            .WithName("LogOut")
            .WithSummary("Signs out an user")
            .RequireAuthorization(ConfigurationConstants.AuthenticationPolicies.Cookie)
            .Produces(StatusCodes.Status302Found)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .AddClosedRequest();
    }

    private static IResult HandleAsync(
        [AsParameters] LogOutParams request,
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
