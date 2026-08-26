using CookBook.IdentityProvider.Api.Endpoints.Users.GetUserProfileInfo.Contracts;
using CookBook.IdentityProvider.Domain.Users.Services.Abstractions;

namespace CookBook.IdentityProvider.Api.Endpoints.Users.GetUserProfileInfo;

public sealed class GetUserProfileInfoModule :
    UsersModule
{
    public override void AddRoutes(
        IEndpointRouteBuilder app)
    {
        app
            .MapGet("/{userName}/profile-info", HandleAsync)
            .WithName("GetUserProfileInfo")
            .WithSummary("Gets the public profile information for user by username")
            .Produces<GetUserProfileInfoResponseDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .AllowAnonymous();
    }

    private static async Task<IResult> HandleAsync(
        [AsParameters]
        GetUserProfileInfoParams request,
        IGetUserProfileInfoQuery getUserProfileInfoQuery,
        ILogger<GetUserProfileInfoModule> logger,
        CancellationToken cancellationToken)
    {
        using var loggerScope = logger.BeginScope(new Dictionary<string, object?>
        {
            ["UserName"] = request.UserName
        });

        try
        {
            var userProfileInfo = await getUserProfileInfoQuery.Execute(
                request.UserName,
                cancellationToken);

            if (userProfileInfo is null)
            {
                return TypedResults.NoContent();
            }

            var responseDto = new GetUserProfileInfoResponseDto
            {
                UserProfileInfo = new GetUserProfileInfoResponseDto.UserProfileInfoDto
                {
                    DisplayName = userProfileInfo.DisplayName,
                    UserName = userProfileInfo.UserName,
                }
            };

            return TypedResults.Ok(responseDto);
        }
        catch (Exception ex)
        when (ex is not OperationCanceledException)
        {
            logger.LogError(
                ex,
                "An unexpected error occurred while getting user profile info");

            throw;
        }
    }
}
