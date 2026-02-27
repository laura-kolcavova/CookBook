using System.ComponentModel.DataAnnotations;

namespace CookBook.IdentityProvider.Api.Users.Endpoints.Login;

public sealed record InputModel
{
    [Required]
    [EmailAddress]
    public required string Email { get; init; }

    [Required]
    public required string Password { get; init; }

    public required bool RememberLogin { get; init; }
}
