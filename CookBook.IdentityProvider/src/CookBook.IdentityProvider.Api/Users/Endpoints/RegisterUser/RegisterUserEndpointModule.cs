using CookBook.IdentityProvider.Api.Shared.Extensions;

namespace CookBook.IdentityProvider.Api.Users.Endpoints.RegisterUser;

public sealed class RegisterUserEndpointModule : UsersModule
{
    public override void AddRoutes(
        IEndpointRouteBuilder app)
    {
        app.MapPost("/register", HandleAsync)
            .WithName("RegisterUser")
            .WithSummary("Registers a user")
            .WithDescription("")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesValidationProblem()
            .WithFluentValidation();
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters]
        RegisterUserEndpointParams request,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        return TypedResults.NoContent();
    }
}
