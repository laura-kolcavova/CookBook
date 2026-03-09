using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenIddict.Abstractions;

namespace CookBook.IdentityProvider.Api.Pages.Home;

[AllowAnonymous]
public sealed class IndexModel :
    PageModel
{
    public UserInfo UserInfo { get; set; } = null!;

    public IActionResult OnGet()
    {
        var isAuthenticated = User
            .Identity
            ?.IsAuthenticated
            ?? false;

        if (!isAuthenticated)
        {
            UserInfo = UserInfo.Anonymous;

            return Page();
        }

        var displayName = User.Claims
            .FirstOrDefault(claim => claim.Type == OpenIddictConstants.Claims.PreferredUsername)
            ?.Value
            ?? throw new InvalidOperationException("Preferred user name is not set.");

        UserInfo = new UserInfo
        {
            IsAuthenticated = true,
            DisplayName = displayName

        };

        return Page();
    }

    //public async Task<IActionResult> OnPostLogOutAsync(
    //    CancellationToken cancellationToken)
    //{
    //    await signInManager.SignOutAsync();

    //    UserInfo = UserInfo.Anonymous;

    //    return Page();
    //}
}