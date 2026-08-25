using CookBook.IdentityProvider.Domain.Users;
using CookBook.IdentityProvider.Domain.Users.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using System.Security.Claims;
using System.Transactions;

namespace CookBook.IdentityProvider.Infrastructure.Users.Services;

internal sealed class ChangeDisplayNameManager(
    UserManager<CustomIdentityUser> userManager,
    UsersContext usersContext) :
    IChangeDisplayNameManager
{
    public async Task ChangeDisplayName(
        int identityUserId,
        string displayName,
        CancellationToken cancellationToken)
    {
        using var transaction = new TransactionScope(
            TransactionScopeOption.Required,
            TransactionScopeAsyncFlowOption.Enabled);

        var identityUser = await userManager.FindByIdAsync(
                identityUserId.ToString())
            ?? throw new InvalidOperationException("Identity user not found.");

        var preferredUsernameClaim = (await userManager.GetClaimsAsync(identityUser))
            .FirstOrDefault(claim => claim.Type == OpenIddictConstants.Claims.PreferredUsername)
            ?? throw new InvalidOperationException("Preferred user name is not set.");

        await userManager.ReplaceClaimAsync(
            identityUser,
            preferredUsernameClaim,
            new Claim(OpenIddictConstants.Claims.PreferredUsername, displayName));

        var user = await usersContext
            .Users
            .SingleAsync(
                user => user.IdentityUserId == identityUserId,
                cancellationToken);

        user.SetDisplayName(displayName);

        await usersContext.SaveChangesAsync(
            cancellationToken);

        transaction.Complete();
    }
}
