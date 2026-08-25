using CookBook.IdentityProvider.Api.Endpoints.Users.ChangeDisplayName.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.IdentityProvider.Api.Endpoints.Users.ChangeDisplayName;

internal sealed record ChangeDisplayNameParams
{
    [FromBody]
    public required ChangeDisplayNameRequestDto ChangeDisplayNameRequest { get; init; }
}
