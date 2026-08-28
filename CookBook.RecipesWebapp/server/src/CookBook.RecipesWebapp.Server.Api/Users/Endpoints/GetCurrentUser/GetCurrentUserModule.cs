using CookBook.Extensions.AspNetCore.Abort.Extensions;
using CookBook.RecipesWebapp.Server.Api.Users.Endpoints.GetCurrentUser.Contracts;
using CookBook.RecipesWebapp.Server.Domain.Users.Services.Abstractions;
using System.Security.Claims;

namespace CookBook.RecipesWebapp.Server.Api.Users.Endpoints.GetCurrentUser;

public sealed class GetCurrentUserEndpointModule :
    UsersModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app
            .MapGet("/current", HandleAsync)
            .WithName("GetCurrentUser")
            .WithSummary("Gets current user info")
            .Produces<GetCurrentUserResponseDto>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .AddClosedRequest();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal? claimsPrincipal,
        ICurrentUserProfileFetcher currentUserProfileFetcher,
        CancellationToken cancellationToken)
    {
        var isAuthenticated = claimsPrincipal
            ?.Identity
            ?.IsAuthenticated
            ?? false;

        if (!isAuthenticated)
        {
            return TypedResults.Ok(
                GetCurrentUserResponseDto.Anonymous);
        }

        var currentUserProfile = await currentUserProfileFetcher.FetchCurrentUserProfile(
            cancellationToken);

        var responseDto = new GetCurrentUserResponseDto
        {
            IsAuthenticated = true,
            UserName = currentUserProfile.UserName,
            DisplayName = currentUserProfile.DisplayName,
            Email = currentUserProfile.Email
        };

        return TypedResults.Ok(
            responseDto);
    }
}
