using System.ComponentModel.DataAnnotations;

namespace CookBook.IdentityProvider.Api.Pages.Users.LogIn;

public sealed class InputModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    public bool RememberMe { get; set; } = false;

    public string ReturnUrl { get; set; } = string.Empty;
}
