using CookBook.IdentityProvider.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CookBook.IdentityProvider.Api.Pages.Users.LogIn;

public sealed class IndexModel(
    SignInManager<CustomIdentityUser> signInManager) :
    PageModel
{
    [BindProperty]
    public InputModel Input { get; set; } = null!;

    private string test = string.Empty;

    public string ReturnUrl { get; set; } = string.Empty;

    public void OnGet(
        [FromQuery]
        string? returnUrl = null)
    {

        ReturnUrl ??= Url.Content("~/");
    }

    public async Task<IActionResult> OnPostAsync(
        string? returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var result = await signInManager.PasswordSignInAsync(
            Input.Email,
            Input.Password,
            Input.RememberMe,
            lockoutOnFailure: false);

        if (result.Succeeded)
        {
            return LocalRedirect(
                returnUrl);
        }
        else
        {
            ModelState.AddModelError(
                string.Empty,
                "The provided email or password is incorrect.");

            return Page();
        }
    }
}