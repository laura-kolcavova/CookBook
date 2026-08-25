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
        CustomIdentityUser identityUser,
        string displayName,
        CancellationToken cancellationToken)
    {
        using var transaction = new TransactionScope(
            TransactionScopeOption.Required,
            TransactionScopeAsyncFlowOption.Enabled);

        var preferredUsernameClaim = (await userManager.GetClaimsAsync(identityUser))
           .Single(claim => claim.Type == OpenIddictConstants.Claims.PreferredUsername)
           ?? throw new InvalidOperationException("Preferred user name is not set.");

        await userManager.ReplaceClaimAsync(
            identityUser,
            preferredUsernameClaim,
            new Claim(OpenIddictConstants.Claims.PreferredUsername, displayName));

        var user = await usersContext
            .Users
            .SingleAsync(
                user => user.IdentityUserId == identityUser.Id,
                cancellationToken);

        user.ChangeDisplayName(displayName);

        await usersContext.SaveChangesAsync(
            cancellationToken);

        transaction.Complete();
    }
}
