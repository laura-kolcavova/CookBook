using CookBook.RecipesWebapp.Server.Application.Users.UseCases.Abstractions;
using CookBook.RecipesWebapp.Server.Domain.Users.Models;
using CookBook.RecipesWebapp.Server.Domain.Users.Services.Abstractions;
using Microsoft.Extensions.Logging;

namespace CookBook.RecipesWebapp.Server.Infrastructure.Users.UseCases;

internal sealed class AuthenticateUserUseCase(
    IAuthenticationManager authenticationManager,
    ILogger<AuthenticateUserUseCase> logger) :
    IAuthenticateUserUseCase
{
    public async Task<AuthenticationResult> AuthenticateUser(
        string email,
        string password,
        CancellationToken cancellationToken)
    {
        try
        {
            var authenticationResult = await authenticationManager.AuthenticateUser(
                email,
                password,
                cancellationToken);

            return authenticationResult;
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
