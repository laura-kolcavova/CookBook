using System.ComponentModel.DataAnnotations;

namespace CookBook.IdentityProvider.Api.Pages.Users.LogIn;

public class InputModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    public bool RememberMe { get; set; }

    public string ReturnUrl { get; set; } = null!;
}
