using Microsoft.AspNetCore.Mvc;

namespace CookBook.IdentityProvider.Api.Endpoints.Users.GetUserProfileInfo;

public sealed record GetUserProfileInfoParams
{
    [FromRoute]
    public required string UserName { get; init; }
}
