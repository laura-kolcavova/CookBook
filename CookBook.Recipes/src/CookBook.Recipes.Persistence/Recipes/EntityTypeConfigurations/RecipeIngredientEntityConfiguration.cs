using CookBook.Recipes.Domain.Recipes.Entities;
using CookBook.Recipes.Persistence.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.Recipes.Persistence.Recipes.EntityTypeConfigurations;

internal sealed class RecipeIngredientEntityConfiguration :
    IEntityTypeConfiguration<RecipeIngredientEntity>
{
    public void Configure(
        EntityTypeBuilder<RecipeIngredientEntity> builder)
    {
        builder.ToTable(
            DboSchema.RecipeIngredientsTableName,
            DboSchema.Name);

        builder
            .HasKey(e => new
            {
                e.RecipeId,
                e.LocalId
            });

        builder
            .Property(e => e.RecipeId)
            .IsRequired();

        builder
            .Property(e => e.LocalId)
            .IsRequired();

        builder
            .Property(e => e.Note)
            .HasMaxLength(256)
            .IsRequired();

        builder
            .Property(e => e.OrderIndex)
            .HasDefaultValue(0)
            .IsRequired();
    }
}
