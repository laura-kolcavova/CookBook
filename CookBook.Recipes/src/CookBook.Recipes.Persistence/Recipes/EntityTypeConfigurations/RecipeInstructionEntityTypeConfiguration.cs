using CookBook.Recipes.Domain.Recipes;
using CookBook.Recipes.Infrastructure.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.Recipes.Persistence.Recipes.EntityTypeConfigurations;

internal sealed class RecipeInstructionEntityTypeConfiguration : IEntityTypeConfiguration<RecipeInstructionEntity>
{
    public void Configure(EntityTypeBuilder<RecipeInstructionEntity> builder)
    {
        builder
            .ToTable(nameof(RecipesContext.RecipeInstructions), RecipesContext.Schema);

        builder
            .HasKey(e => new
            {
                e.RecipeId,
                e.LocalId,
            });

        builder
            .Property(e => e.RecipeId)
            .IsRequired();

        builder
            .Property(e => e.Note)
            .HasMaxLength(1024)
            .IsRequired();

        builder
            .Property(e => e.OrderIndex)
            .HasDefaultValue(0)
            .IsRequired();
    }
}
