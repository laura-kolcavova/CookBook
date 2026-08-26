using Microsoft.AspNetCore.Mvc;

namespace CookBook.RecipesWebapp.Server.Api.Users.Endpoints.LogIn;

internal sealed record LogInParams
{
    [FromQuery]
    public string? ReturnUrl { get; init; }
}
