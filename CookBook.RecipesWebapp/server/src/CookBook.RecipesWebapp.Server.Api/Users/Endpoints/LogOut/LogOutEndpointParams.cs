using Microsoft.AspNetCore.Mvc;

namespace CookBook.RecipesWebapp.Server.Api.Users.Endpoints.LogOut;

internal sealed record LogOutEndpointParams
{
    [FromQuery]
    public string? ReturnUrl { get; init; }
}
