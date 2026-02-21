
using CookBook.RecipesWebapp.Server.Api.Shared.Extensions;
using CookBook.RecipesWebapp.Server.Application.Users.UseCases.Abstractions;

namespace CookBook.RecipesWebapp.Server.Api.Users.Endpoints.LogIn;

public sealed class LogInEndpointModule :
    UsersModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapPost("/login", HandleAsync)
            .WithName("LogIn")
            .WithSummary("Signs in a user")
            .WithDescription("")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem()
            .WithFluentValidation();
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters] LogInEndpointParams request,
        ILogInUseCase logInUseCase,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var logInRequest = request.LogInRequest;

        await logInUseCase.LogIn(
            logInRequest.Email,
            logInRequest.Password,
            cancellationToken);

        return TypedResults.NoContent();
    }
}
