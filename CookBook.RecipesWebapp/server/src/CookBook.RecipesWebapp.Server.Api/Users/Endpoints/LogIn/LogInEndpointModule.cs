using CookBook.Extensions.AspNetCore.FluentValidation.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace CookBook.RecipesWebapp.Server.Api.Users.Endpoints.LogIn;

public sealed class LogInEndpointModule :
    UsersModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapGet("/login", Handle)
            .WithName("LogIn")
            .WithSummary("Signs in an user")
            .WithDescription("")
            .Produces(StatusCodes.Status307TemporaryRedirect)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .AddFluentValidation()
            .AllowAnonymous()
            .DisableAntiforgery();
    }

    private static IResult Handle(
        [AsParameters] LogInEndpointParams request)
    {
        var properties = new AuthenticationProperties
        {
            RedirectUri = BuildReturnUrl(request.ReturnUrl),
        };

        return TypedResults.Challenge(
            properties,
            authenticationSchemes: [
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
