using CookBook.Extensions.CSharpExtended.Errors;
using CookBook.RecipesWebapp.Server.Domain.Users.Models;
using CSharpFunctionalExtensions;

namespace CookBook.RecipesWebapp.Server.Domain.Users.Services.Abstractions;

public interface IAuthenticationManager
{
    public Task<Result<AuthenticationResult, Error>> AuthenticateUser(
        string email,
        string password,
        CancellationToken cancellationToken);
}
