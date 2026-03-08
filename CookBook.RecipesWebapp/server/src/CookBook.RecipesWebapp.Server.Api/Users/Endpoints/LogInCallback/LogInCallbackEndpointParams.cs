using Microsoft.AspNetCore.Mvc;

namespace CookBook.RecipesWebapp.Server.Api.Users.Endpoints.LogIn;

internal sealed record LogInCallbackEndpointParams
{
    [FromRoute]
    public string Provider { get; init; } = string.Empty;
}
