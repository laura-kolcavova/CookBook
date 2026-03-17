using CookBook.IdentityProvider.Api.Endpoints.Users.GetUserProfileInfo.Contracts;
using CookBook.IdentityProvider.Application.Users.UseCases.GetUserProfileInfo;

namespace CookBook.IdentityProvider.Api.Endpoints.Users.GetUserProfileInfo;

public sealed class GetUserProfileInfoEndpointModule :
    UsersModule
{

    public override void AddRoutes(
        IEndpointRouteBuilder app)
    {
        app
            .MapGet("/{userName}/info", HandleAsync)
            .WithName("GetUserProfileInfo")
            .WithSummary("Retrieves user profile information by username")
            .WithDescription("Gets the public profile information for a user including their display name and username. Returns 204 No Content if the user is not found.")
            .Produces<GetUserProfileInfoResponseDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .AllowAnonymous();
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters]
        GetUserProfileInfoEndpointParams request,
        IGetUserProfileInfoUseCase getUserProfileInfoUseCase,
        CancellationToken cancellationToken)
    {
        var userProfileInfoResult = await getUserProfileInfoUseCase.GetUserProfileInfo(
            request.UserName,
            cancellationToken);

        if (userProfileInfoResult.HasNoValue)
        {
            return TypedResults.NoContent();
        }

        var userProfileInfo = userProfileInfoResult.Value;

        var responseDto = new GetUserProfileInfoResponseDto
        {
            UserProfileInfo = new UserProfileInfoDto
            {
                DisplayName = userProfileInfo.DisplayName,
                UserName = userProfileInfo.UserName,
            }
        };

        return TypedResults.Ok(responseDto);
    }

}
