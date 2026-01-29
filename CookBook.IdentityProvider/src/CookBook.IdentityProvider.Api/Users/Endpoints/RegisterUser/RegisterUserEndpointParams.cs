using CookBook.IdentityProvider.Api.Users.Endpoints.RegisterUser.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.IdentityProvider.Api.Users.Endpoints.RegisterUser;

internal sealed record RegisterUserEndpointParams
{
    [FromBody]
    public required RegisterUserRequestDto RegisterUserRequest { get; init; }
}
