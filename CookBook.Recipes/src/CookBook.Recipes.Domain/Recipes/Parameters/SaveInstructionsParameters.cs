﻿namespace CookBook.Recipes.Domain.Recipes.Parameters;

public sealed record SaveInstructionsParameters
{
    public required IEnumerable<InstructionParameters> Instructions { get; init; }

    public record InstructionParameters
    {
        public long? LocalId { get; init; } = default;

        public required string Note { get; init; }
    }
}
