using Microsoft.AspNetCore.Mvc;

namespace CookBook.IdentityProvider.Api.Endpoints.Users.GetUserProfileInfo;

public sealed record GetUserProfileInfoEndpointParams
{
    [FromRoute]
    public required string UserName { get; init; }
}
