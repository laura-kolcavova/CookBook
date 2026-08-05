using CookBook.IdentityProvider.Domain.Users;
using CookBook.IdentityProvider.Domain.Users.Models;
using CookBook.IdentityProvider.Domain.Users.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CookBook.IdentityProvider.Api.Pages.Account.Register;

[AllowAnonymous]
public class IndexModel(
    IRegisterManager registerManager,
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
            ["DisplayName"] = Input.DisplayName,
            ["Email"] = Input.Email
        });

        try
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var registerUserRequest = new RegisterUserRequest
            {
                DisplayName = Input.DisplayName,
                Email = Input.Email,
                Password = Input.Password
            };

            var registerUserResult  = await registerManager.RegisterUser(
                registerUserRequest,
                cancellationToken);

            if (registerUserResult.IsFailure)
            {
                ModelState.AddModelError(
                    registerUserResult.Error.Code,
                    registerUserResult.Error.Message);

                return Page();
            }

            var identityUser = registerUserResult.Value.IdentityUser;

            await signInManager.SignInAsync(
                identityUser,
                isPersistent: false);

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
                "An unexpected error occurred while registering an user");

            throw;
        }
    }
}
