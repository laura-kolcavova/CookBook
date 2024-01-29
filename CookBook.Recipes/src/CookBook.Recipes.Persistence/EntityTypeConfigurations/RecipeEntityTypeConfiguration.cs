using CookBook.Recipes.Domain.Recipes;
using CookBook.Recipes.Infrastructure.DatabaseContexts;
using CookBook.Recipes.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.Recipes.Persistence.EntityTypeConfigurations;
internal class RecipeEntityTypeConfiguration : IEntityTypeConfiguration<RecipeEntity>
{
    public void Configure(EntityTypeBuilder<RecipeEntity> builder)
    {
        builder
            .ToTable(nameof(RecipesContext.Recipes), RecipesContext.Schema);

        builder
            .HasKey(e => e.Id);

        builder
            .Property(e => e.UserId)
            .IsRequired();

        builder
            .Property(e => e.Title)
            .HasMaxLength(256)
            .IsRequired();

        builder
            .Property(e => e.Description)
            .HasMaxLength(1024);

        builder
            .Property(e => e.Notes)
            .HasMaxLength(1024);

        builder
            .Property(e => e.Servings)
            .HasDefaultValue(0)
            .IsRequired();

        builder
            .Property(e => e.PreparationTime)
            .HasDefaultValue(0)
            .IsRequired();

        builder
            .Property(e => e.CookTime)
            .HasDefaultValue(0)
            .IsRequired();

        builder
            .HasTrackableProperties();

        builder
            .HasMany(e => e.Ingredients)
            .WithOne()
            .HasForeignKey(f => f.RecipeId);

        builder
            .HasMany(e => e.Instructions)
            .WithOne()
            .HasForeignKey(f => f.RecipeId);
    }
}
