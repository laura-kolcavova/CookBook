using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.IdentityProvider.Domain.Users.Models;
using CSharpFunctionalExtensions;

namespace CookBook.IdentityProvider.Domain.Users.UseCases.Abstractions;

public interface IRegisterUserUseCase
{
    public Task<UnitResult<Error>> RegisterUser(
        RegisterUserRequest registerUserRequest,
        CancellationToken cancellationToken);
}
