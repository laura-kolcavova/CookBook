namespace CookBook.RecipesWebapp.Server.Api.Users.Endpoints.LogIn.Contracts;

internal sealed record LoginRequestDto
{
    public required string Email { get; init; }

    public required string Password { get; init; }
}
