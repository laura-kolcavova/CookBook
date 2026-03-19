using CookBook.IdentityProvider.Domain.Users.ReadModels;
using CookBook.IdentityProvider.Domain.Users.Services.Abstractions;
using CookBook.IdentityProvider.Infrastructure.Users.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CookBook.IdentityProvider.Infrastructure.Users.Services;

internal sealed class GetUserProfileInfoQuery(
    UsersContext usersContext) :
    IGetUserProfileInfoQuery
{
    public async Task<UserProfileInfoReadModel?> Execute(
        string userName,
        CancellationToken cancellationToken)
    {
        var userProfileInfo = await usersContext
            .Users
            .AsNoTracking()
            .Where(user => user.UserName == userName)
            .ProjectToUserProfileInfoReadModel()
            .SingleOrDefaultAsync(cancellationToken);

        return userProfileInfo;
    }
}
