using CookBook.RecipesWebapp.Server.Domain.Users.Models;

namespace CookBook.RecipesWebapp.Server.Application.Users.UseCases.Abstractions;

public interface IAuthenticateUserUseCase
{
    public Task<AuthenticationResult> AuthenticateUser(
        string email,
        string password,
        CancellationToken cancellationToken);
}
