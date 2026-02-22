using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.IdentityProvider.Domain.Users;
using CookBook.IdentityProvider.Domain.Users.Models;
using CookBook.IdentityProvider.Domain.Users.Services.Abstractions;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Transactions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace CookBook.IdentityProvider.Infrastructure.Users.Services;

internal sealed class RegisterManager(
    UserManager<CustomIdentityUser> userManager,
    UsersContext usersContext) :
    IRegisterManager
{
    public async Task<Result<UserAggregate, Error>> RegisterUser(
        RegisterUserRequest registerUserRequest,
        CancellationToken cancellationToken)
    {
        using var transaction = new TransactionScope(
            TransactionScopeOption.Required,
            TransactionScopeAsyncFlowOption.Enabled);

        var userName = Guid
            .NewGuid()
            .ToString();

        var identityUser = new CustomIdentityUser()
        {
            UserName = userName,
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

        var claimsResult = await userManager.AddClaimAsync(
            identityUser,
            new Claim(
                Claims.PreferredUsername,
                registerUserRequest.DisplayName));

        if (!claimsResult.Succeeded)
        {
            var identityError = claimsResult
                .Errors
                .First();

            var error = Error.Failure(
                code: identityError.Code,
                message: identityError.Description);

            return error;
        }

        var user = new UserAggregate(
            identityUserId: identityUser.Id,
            userName: userName,
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
}
