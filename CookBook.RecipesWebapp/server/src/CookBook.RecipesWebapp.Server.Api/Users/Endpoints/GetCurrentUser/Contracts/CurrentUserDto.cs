namespace CookBook.RecipesWebapp.Server.Api.Users.Endpoints.GetCurrentUser.Contracts;

public sealed record CurrentUserDto
{
    public required bool IsAuthenticated { get; init; }

    public required string DisplayName { get; init; }

    public required string Email { get; init; }

    public static CurrentUserDto Anonymous => new()
    {
        IsAuthenticated = false,
        DisplayName = string.Empty,
        Email = string.Empty
    };
}
