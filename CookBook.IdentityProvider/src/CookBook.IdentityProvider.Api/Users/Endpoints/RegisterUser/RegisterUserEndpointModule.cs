using CookBook.Extensions.AspNetCore.Errors;
using CookBook.IdentityProvider.Api.Shared.Extensions;
using CookBook.IdentityProvider.Api.Users.Endpoints.RegisterUser.Mappers;
using CookBook.IdentityProvider.Application.Users.UseCases.RegisterUser.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.IdentityProvider.Api.Users.Endpoints.RegisterUser;

public sealed class RegisterUserEndpointModule : UsersModule
{
    public override void AddRoutes(
        IEndpointRouteBuilder app)
    {
        app
            .MapPost("/register", HandleAsync)
            .WithName("RegisterUser")
            .WithSummary("Registers a user")
            .WithDescription("")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesValidationProblem()
            .WithFluentValidation();
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters] RegisterUserEndpointParams request,
        [FromServices] IRegisterUserUseCase registerUserUseCase,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var registerUserRequest = request
            .RegisterUserRequest
            .ToModel();

        var registerUserResult = await registerUserUseCase.RegisterUser(
            registerUserRequest,
            cancellationToken);

        if (registerUserResult.IsFailure)
        {
            return TypedResults.Problem(
                registerUserResult
                    .Error
                    .AsProblemDetails(httpContext));
        }

        return TypedResults.NoContent();
    }
}
