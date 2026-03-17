using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.IdentityProvider.Domain.Users.Models;
using CSharpFunctionalExtensions;

namespace CookBook.IdentityProvider.Application.Users.UseCases.RegisterUser;

public interface IRegisterUserUseCase
{
    public Task<Result<RegisterUserResult, Error>> RegisterUser(
        RegisterUserRequest registerUserRequest,
        CancellationToken cancellationToken);
}
