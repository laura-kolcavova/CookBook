using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.IdentityProvider.Application.Users.UseCases.RegisterUser.Abstractions;
using CookBook.IdentityProvider.Domain.Users;
using CookBook.IdentityProvider.Domain.Users.Models;
using CookBook.IdentityProvider.Domain.Users.Services.Abstractions;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace CookBook.IdentityProvider.Infrastructure.Users.UseCases.RegisterUser;

internal sealed class RegisterUserUseCase(
    IRegisterManager registerManager,
    ILogger<RegisterUserUseCase> logger) :
    IRegisterUserUseCase
{
    public async Task<Result<UserAggregate, Error>> RegisterUser(
        RegisterUserRequest registerUserRequest,
        CancellationToken cancellationToken)
    {
        using var loggerScope = logger.BeginScope(new Dictionary<string, object?>
        {
            ["DisplayName"] = registerUserRequest.DisplayName,
            ["Email"] = registerUserRequest.Email
        });

        try
        {
            return await registerManager.RegisterUser(
                registerUserRequest,
                cancellationToken);
        }
        catch (Exception ex)
        when (ex is not OperationCanceledException)
        {
            logger.LogError(
                ex,
                "An unexpected error occurred while getting recipe detail.");

            throw;
        }
    }
}
