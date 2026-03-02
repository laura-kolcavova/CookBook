using CookBook.IdentityProvider.Application.Users.UseCases.RegisterUser.Abstractions;
using CookBook.IdentityProvider.Domain.Users;
using CookBook.IdentityProvider.Domain.Users.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CookBook.IdentityProvider.Api.Pages.Account.Register
{
    [AllowAnonymous]
    public class IndexModel(
        IRegisterUserUseCase registerUserUseCase,
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

        public async Task<IActionResult> OnPostAsync(
            CancellationToken cancellationToken)
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

            var registerUserResult = await registerUserUseCase.RegisterUser(
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

            return Redirect(Input.ReturnUrl);
        }
    }
}
