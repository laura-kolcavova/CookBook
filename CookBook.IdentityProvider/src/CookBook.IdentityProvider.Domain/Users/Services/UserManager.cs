using CookBook.IdentityProvider.Domain.Users.Services.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace CookBook.IdentityProvider.Domain.Users.Services;

internal sealed class UserManager(
    IUserStore<UserAggregate> userStore) : IUserManager
{
}
