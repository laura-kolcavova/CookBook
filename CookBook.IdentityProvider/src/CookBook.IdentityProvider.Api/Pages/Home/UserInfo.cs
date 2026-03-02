namespace CookBook.IdentityProvider.Api.Pages.Home;

public sealed record UserInfo
{
    public required bool IsAuthenticated { get; init; }

    public required string DisplayName { get; init; }

    public static UserInfo Anonymous => new()
    {
        IsAuthenticated = false,
        DisplayName = string.Empty,
    };
}
