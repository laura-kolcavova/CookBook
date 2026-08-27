using CookBook.Extensions.CSharpExtended.Errors;
using CSharpFunctionalExtensions;

namespace CookBook.IdentityProvider.Domain.Users.Services.Abstractions;

public interface IChangeDisplayNameManager
{
    public Task<UnitResult<Error>> ChangeDisplayName(
        CustomIdentityUser identityUser,
        string displayName,
        CancellationToken cancellationToken);
}
