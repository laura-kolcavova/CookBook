using CookBook.IdentityProvider.Api.Endpoints.Users.RegisterUser.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.IdentityProvider.Api.Endpoints.Users.RegisterUser;

internal sealed record RegisterUserEndpointParams
{
    [FromBody]
    public required RegisterUserRequestDto RegisterUserRequest { get; init; }
}
