using CookBook.IdentityProvider.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CookBook.IdentityProvider.Api.Pages.Account.LogIn;

[AllowAnonymous]
public sealed class IndexModel(
    UserManager<CustomIdentityUser> userManager,
    SignInManager<CustomIdentityUser> signInManager,
    ILogger<IndexModel> logger) :
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

    public async Task<IActionResult> OnPostAsync(
        CancellationToken cancellationToken)
    {
        using var loggerScope = logger.BeginScope(new Dictionary<string, object?>
        {
            ["Email"] = Input.Email
        });

        try
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

            var signInResult = await signInManager.PasswordSignInAsync(
                user,
                Input.Password!,
                Input.RememberMe,
                lockoutOnFailure: false);

            if (!signInResult.Succeeded)
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

            return LocalRedirect(Input.ReturnUrl);
        }
        catch (Exception ex)
        when (ex is not OperationCanceledException)
        {
            logger.LogError(
                ex,
                "An unexpected error occurred while signing an user");

            throw;
        }
    }
}
