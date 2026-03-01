using CookBook.RecipesWebapp.Server.Api.Shared.Extensions;
using CookBook.RecipesWebapp.Server.Application.Users.UseCases.Abstractions;
using Microsoft.AspNetCore.Authentication;
using OpenIddict.Client.AspNetCore;

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
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem()
            .ValidateRequest()
            .AllowAnonymous();
    }

    private static IResult Handle(
        [AsParameters] LogInEndpointParams request,
        IAuthenticateUserUseCase authenticateUserUseCase,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var properties = new AuthenticationProperties
        {
            RedirectUri = BuildReturnUrl(request.ReturnUrl)
        };

        return TypedResults.Challenge(
            properties,
            [
                OpenIddictClientAspNetCoreDefaults.AuthenticationScheme
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
        else if (!Uri.IsWellFormedUriString(returnUrl, UriKind.Relative))
        {
            var uri = new Uri(
                returnUrl,
                UriKind.Absolute);

            return uri.PathAndQuery;
        }
        else if (returnUrl[0] != '/')
        {
            return $"{pathBase}{returnUrl}";
        }
        else
        {
            return returnUrl;
        }
    }
}
