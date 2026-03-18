namespace CookBook.RecipesWebapp.Server.Domain.Recipes.Models;

public sealed record RecipeDetailModel
{
    public required long RecipeId { get; init; }

    public required string Title { get; init; }

    public required string? Description { get; init; }

    public required short Servings { get; init; }

    public required short CookTime { get; init; }

    public required string? Notes { get; init; }

    public required DateTimeOffset CreatedAt { get; init; }

    public required UserProfileInfoModel UserProfileInfo { get; init; }

    public required IReadOnlyCollection<IngredientItemModel> Ingredients { get; init; }

    public required IReadOnlyCollection<InstructionItemModel> Instructions { get; init; }

    public required IReadOnlyCollection<string> Tags { get; init; }

    public sealed record UserProfileInfoModel
    {
        public required string DisplayName { get; init; }

        public required string UserName { get; init; }
    }

    public sealed record IngredientItemModel
    {
        public required int LocalId { get; init; }

        public required string Note { get; init; }
    }

    public sealed record InstructionItemModel
    {
        public required int LocalId { get; init; }

        public required string Note { get; init; }
    }
}
