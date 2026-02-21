using CookBook.RecipesWebapp.Server.Domain.Users.Models;

namespace CookBook.RecipesWebapp.Server.Domain.Users.Services.Abstractions;

public interface IAuthenticationManager
{
    public Task<AuthenticationResult> AuthenticateUser(
        string email,
        string password,
        CancellationToken cancellationToken);
}
