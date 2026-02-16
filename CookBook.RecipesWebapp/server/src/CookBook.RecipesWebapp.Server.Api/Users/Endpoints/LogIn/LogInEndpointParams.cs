using CookBook.RecipesWebapp.Server.Api.Users.Endpoints.LogIn.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.RecipesWebapp.Server.Api.Users.Endpoints.LogIn;

internal sealed record LogInEndpointParams
{
    [FromBody]
    public required LoginRequestDto LogInRequest { get; init; }
}
