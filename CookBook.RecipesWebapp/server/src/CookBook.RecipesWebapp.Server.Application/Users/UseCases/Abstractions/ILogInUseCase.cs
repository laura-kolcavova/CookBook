namespace CookBook.RecipesWebapp.Server.Application.Users.UseCases.Abstractions;

public interface ILogInUseCase
{
    public Task LogIn(
        string email,
        string password,
        CancellationToken cancellationToken);
}
