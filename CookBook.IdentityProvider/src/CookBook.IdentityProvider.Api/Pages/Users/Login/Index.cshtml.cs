using CookBook.IdentityProvider.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CookBook.IdentityProvider.Api.Pages.Users.LogIn;

public sealed class IndexModel(
    UserManager<CustomIdentityUser> userManager,
    SignInManager<CustomIdentityUser> signInManager) :
    PageModel
{
    [BindProperty]
    public InputModel Input { get; set; } = null!;

    public IActionResult OnGet(
        [FromQuery]
        string? returnUrl = null)
    {
        Input = new InputModel
        {
            ReturnUrl = returnUrl ?? Url.Content("~/"),
        };

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = await userManager.FindByEmailAsync(
            Input.Email);

        if (user is null)
        {
            ModelState.AddModelError(
               string.Empty,
               "The email/password couple is invalid.");

            return Page();
        }

        var result = await signInManager.PasswordSignInAsync(
            user,
            Input.Password!,
            Input.RememberMe,
            lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            ModelState.AddModelError(
               string.Empty,
               "The email/password couple is invalid.");

            return Page();
        }

        if (string.IsNullOrEmpty(Input.ReturnUrl))
        {
            return Redirect("~/");
        }

        if (!Url.IsLocalUrl(Input.ReturnUrl))
        {
            return Redirect("~/");
        }

        return Redirect(Input.ReturnUrl);
    }
}