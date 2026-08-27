using Microsoft.AspNetCore.Mvc;

namespace CookBook.RecipesWebapp.Server.Api.Users.Endpoints.LogOut;

internal sealed record LogOutParams
{
    [FromQuery]
    public string? ReturnUrl { get; init; }
}
