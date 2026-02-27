using System.ComponentModel.DataAnnotations;

namespace CookBook.IdentityProvider.Api.Pages.Users.Login;

public sealed record InputModel
{
    [Required]
    [EmailAddress]
    public required string Email { get; init; }

    [Required]
    public required string Password { get; init; }

    public required bool RememberLogin { get; init; }
}
