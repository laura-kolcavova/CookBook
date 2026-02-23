using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.RecipesWebapp.Server.Application.Users.UseCases.Abstractions;
using CookBook.RecipesWebapp.Server.Domain.Users.Models;
using CookBook.RecipesWebapp.Server.Domain.Users.Services.Abstractions;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace CookBook.RecipesWebapp.Server.Application.Users.UseCases;

internal sealed class AuthenticateUserUseCase(
    IAuthenticationManager authenticationManager,
    ILogger<AuthenticateUserUseCase> logger) :
    IAuthenticateUserUseCase
{
    public async Task<Result<AuthenticationResult, Error>> AuthenticateUser(
        string email,
        string password,
        CancellationToken cancellationToken)
    {
        using var loggerScope = logger.BeginScope(new Dictionary<string, object?>
        {
            ["Email"] = email
        });

        try
        {
            var result = await authenticationManager.AuthenticateUser(
                email,
                password,
                cancellationToken);

            return result;
        }
        catch (Exception ex)
        when (ex is not OperationCanceledException)
        {
            logger.LogError(
                "An unexpected error occurred while authenticating an user");

            throw;
        }
    }
}
