using CookBook.Recipes.Domain.Entities.Recipes;
using CookBook.Recipes.Infrastructure.DatabaseContexts;
using CookBook.Recipes.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.Recipes.Persistence.EntityTypeConfigurations;

internal class RecipeIngredientEntityTypeConfiguration : IEntityTypeConfiguration<RecipeIngredientEntity>
{
    public void Configure(EntityTypeBuilder<RecipeIngredientEntity> builder)
    {
        builder
            .ToTable(nameof(RecipesContext.RecipeIngredients), RecipesContext.Schema);

        builder
            .HasKey(e => e.Id);

        builder
            .Property(e => e.RecipeId)
            .IsRequired();

        builder
            .Property(e => e.Note)
            .HasMaxLength(256)
            .IsRequired();

        builder
            .Property(e => e.OrderIndex)
            .HasDefaultValue(0)
            .IsRequired();

        builder
            .HasTrackableProperties();
    }
}
