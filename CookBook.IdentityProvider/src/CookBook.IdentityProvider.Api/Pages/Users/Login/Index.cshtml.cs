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

    public IActionResult OnGet(
        [FromQuery]
        string? returnUrl = null)
    {
        Input = new InputModel
        {
            ReturnUrl = returnUrl ?? Url.Content("~/")
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var result = await signInManager.PasswordSignInAsync(
            Input.Email!,
            Input.Password,
            Input.RememberMe,
            lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            ModelState.AddModelError(
               string.Empty,
               "The provided email or password is incorrect.");

            return Page();
        }


        if (string.IsNullOrEmpty(Input.ReturnUrl))
        {
            return Redirect("~/");
        }
        if (Url.IsLocalUrl(Input.ReturnUrl))
        {
            return Redirect(Input.ReturnUrl);
        }
        else
        {
            // user might have clicked on a malicious link - should be logged
            throw new Exception("invalid return URL");
        }
    }
}