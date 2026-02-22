using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.IdentityProvider.Domain.Users.Models;
using CSharpFunctionalExtensions;

namespace CookBook.IdentityProvider.Domain.Users.Services.Abstractions;

public interface IRegisterManager
{
    public Task<Result<UserAggregate, Error>> RegisterUser(
        RegisterUserRequest registerUserRequest,
        CancellationToken cancellationToken);
}
