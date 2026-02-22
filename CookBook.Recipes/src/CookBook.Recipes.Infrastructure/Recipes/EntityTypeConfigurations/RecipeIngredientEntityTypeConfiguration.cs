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
            .HasColumnType("BIGINT")
            .IsRequired();

        builder
            .Property(e => e.LocalId)
            .HasColumnType("INT")
            .IsRequired();

        builder
            .Property(e => e.Note)
            .HasColumnType("NVARCHAR(256)")
            .HasMaxLength(256)
            .IsRequired();

        builder
            .Property(e => e.OrderIndex)
            .HasColumnType("SMALLINT")
            .HasDefaultValue(0)
            .IsRequired();
    }
}
