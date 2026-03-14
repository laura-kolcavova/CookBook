namespace CookBook.Recipes.Api.Recipes.Endpoints.GetRecipeDetail.Contracts;

internal sealed record GetRecipeDetailResponseDto
{
    public required RecipeDetailDto RecipeDetail { get; init; }
}
