using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.RecipesWebapp.Server.Domain.Users.Models;
using CSharpFunctionalExtensions;

namespace CookBook.RecipesWebapp.Server.Application.Users.UseCases.Abstractions;

public interface IAuthenticateUserUseCase
{
    public Task<Result<AuthenticationResult, Error>> AuthenticateUser(
        string email,
        string password,
        CancellationToken cancellationToken);
}
