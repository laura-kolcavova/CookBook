using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CookBook.IdentityProvider.Api.Users.Endpoints.Login;

public sealed class IndexModel :
    PageModel
{
    [BindProperty]
    public InputModel Input { get; set; } = null!;

    public void OnGet(
        [FromQuery]
        string? returnUrl = null)
    {
    }
}