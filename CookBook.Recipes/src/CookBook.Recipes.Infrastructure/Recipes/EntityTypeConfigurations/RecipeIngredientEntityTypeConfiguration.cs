using CookBook.Recipes.Domain.Recipes.Entities;
using CookBook.Recipes.Infrastructure.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.Recipes.Infrastructure.Recipes.EntityTypeConfigurations;

internal sealed class RecipeIngredientEntityTypeConfiguration :
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
