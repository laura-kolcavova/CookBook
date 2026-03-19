using CookBook.IdentityProvider.Domain.Users;
using CookBook.IdentityProvider.Domain.Users.ReadModels;

namespace CookBook.IdentityProvider.Infrastructure.Users.Extensions;

internal static class UserProfileInfoReadModelExtensions
{
    public static IQueryable<UserProfileInfoReadModel> ProjectToUserProfileInfoReadModel(
        this IQueryable<UserAggregate> users)
    {
        return users
           .Select(user => new UserProfileInfoReadModel
           {
               UserName = user.UserName,
               DisplayName = user.DisplayName,
           });
    }
}
