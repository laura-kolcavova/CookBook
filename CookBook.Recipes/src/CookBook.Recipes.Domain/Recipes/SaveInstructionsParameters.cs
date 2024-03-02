namespace CookBook.Recipes.Domain.Recipes;

public record SaveInstructionsParameters
{
    public required IEnumerable<InstructionParameters> Instructions { get; init; }

    public record InstructionParameters
    {
        public long Id { get; init; } = default;

        public required string Note { get; init; }
    }
}
