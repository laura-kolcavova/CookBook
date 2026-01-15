namespace CookBook.Recipes.Api.Recipes.Endpoints.GetLatestRecipes.Contracts;

internal sealed record GetLatestRecipesResponseDto
{
    public required IReadOnlyCollection<LatestRecipeDto> LatestRecipes { get; init; }
}
