namespace CookBook.Recipes.Domain.Recipes.Parameters;

public sealed record SaveIngredientsParameters
{
    public required IEnumerable<IngredientParameters> Ingredients { get; init; }

    public record IngredientParameters
    {
        public long? Id { get; init; } = default;

        public required string Note { get; init; }
    }
}
