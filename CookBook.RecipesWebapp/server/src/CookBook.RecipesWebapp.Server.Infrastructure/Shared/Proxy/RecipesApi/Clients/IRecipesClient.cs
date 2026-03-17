using CookBook.RecipesWebapp.Server.Infrastructure.Shared.Proxy.RecipesApi.Dto;
using Refit;

namespace CookBook.RecipesWebapp.Server.Infrastructure.Shared.Proxy.RecipesApi.Clients;

internal interface IRecipesClient
{
    [Get("/api/recipes/{recipeId}/detail")]
    public Task<IApiResponse<GetRecipeDetailResponseDto>> GetRecipeDetail(
        int recipeId,
        CancellationToken cancellationToken);
}
