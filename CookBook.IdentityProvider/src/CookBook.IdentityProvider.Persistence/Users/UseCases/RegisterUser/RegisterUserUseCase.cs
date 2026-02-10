using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.IdentityProvider.Application.Users.UseCases.RegisterUser;
using CookBook.IdentityProvider.Application.Users.UseCases.RegisterUser.Abstractions;
using CookBook.IdentityProvider.Domain.Users;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Transactions;

namespace CookBook.IdentityProvider.Persistence.Users.UseCases.RegisterUser;

internal sealed class RegisterUserUseCase(
    UserManager<IdentityUser<int>> userManager,
    UsersContext usersContext,
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
            using var transaction = new TransactionScope(
                TransactionScopeOption.Required,
                TransactionScopeAsyncFlowOption.Enabled);

            var userNumber = Guid.NewGuid();

            var identityUser = new IdentityUser<int>()
            {
                UserName = userNumber.ToString(),
                Email = registerUserRequest.Email,
            };

            var identityResult = await userManager.CreateAsync(
                identityUser,
                registerUserRequest.Password);

            if (!identityResult.Succeeded)
            {
                var identityError = identityResult
                    .Errors
                    .First();

                var error = Error.Failure(
                    code: identityError.Code,
                    message: identityError.Description);

                return error;
            }

            var user = new UserAggregate(
                identityUserId: identityUser.Id,
                userNumber: userNumber,
                displayName: registerUserRequest.DisplayName);

            await usersContext
                .Users
                .AddAsync(
                    user,
                    cancellationToken);

            await usersContext.SaveChangesAsync(
                cancellationToken);

            transaction.Complete();

            return user;
        }
        catch (Exception ex)
        when (ex is not TaskCanceledException)
        {
            logger.LogError(
                ex,
                "An unexpected error occurred while getting recipe detail");

            throw;
        }
    }
}
