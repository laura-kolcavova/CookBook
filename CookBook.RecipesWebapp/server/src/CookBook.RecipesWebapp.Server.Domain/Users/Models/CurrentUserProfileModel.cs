namespace CookBook.RecipesWebapp.Server.Domain.Users.Models;

public sealed record CurrentUserProfileModel
{
    public required string DisplayName { get; init; }

    public required string Email { get; init; }
}
