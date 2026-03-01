using System.ComponentModel.DataAnnotations;

namespace CookBook.IdentityProvider.Api.Pages.Account.LogIn;

public sealed class InputModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    public bool RememberMe { get; set; }

    public string ReturnUrl { get; set; } = null!;
}
