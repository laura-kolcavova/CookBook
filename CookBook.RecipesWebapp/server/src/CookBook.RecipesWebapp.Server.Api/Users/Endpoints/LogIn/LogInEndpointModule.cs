using CookBook.Extensions.AspNetCore.Errors;
using CookBook.RecipesWebapp.Server.Api.Shared.Extensions;
using CookBook.RecipesWebapp.Server.Application.Users.UseCases.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CookBook.RecipesWebapp.Server.Api.Users.Endpoints.LogIn;

public sealed class LogInEndpointModule :
    UsersModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapPost("/login", HandleAsync)
            .WithName("LogIn")
            .WithSummary("Signs in an user")
            .WithDescription("")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem()
            .ValidateRequest()
            .HandleOperationCancelled()
            .AllowAnonymous();
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters] LogInEndpointParams request,
        IAuthenticateUserUseCase authenticateUserUseCase,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var logInRequest = request.LogInRequest;

        var authenticationResult = await authenticateUserUseCase.AuthenticateUser(
            logInRequest.Email,
            logInRequest.Password,
            cancellationToken);

        if (authenticationResult.IsFailure)
        {
            return TypedResults.Problem(
                authenticationResult
                    .Error
                    .AsProblemDetails(httpContext));
        }

        var authProperties = new AuthenticationProperties
        {
        };

        await httpContext.SignInAsync(
            scheme: CookieAuthenticationDefaults.AuthenticationScheme,
            principal: authenticationResult
                .Value
                .UserInfoTokenPrincipal,
            properties: authProperties);

        return TypedResults.NoContent();
    }
}
