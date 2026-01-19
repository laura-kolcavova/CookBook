using CookBook.Recipes.Domain.Recipes;
using CookBook.Recipes.Persistence.Shared.Constants;
using CookBook.Recipes.Persistence.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.Recipes.Persistence.Recipes.EntityTypeConfigurations;

internal sealed class RecipeAggregateRootConfiguration :
    IEntityTypeConfiguration<RecipeAggregate>
{
    public void Configure(EntityTypeBuilder<RecipeAggregate> builder)
    {
        builder.ToTable(
            DboSchema.RecipesTableName,
            DboSchema.Name);

        builder
            .HasKey(e => e.Id);

        builder
            .Property(e => e.Id)
            .IsRequired();

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

        builder
            .HasMany(e => e.RecipeTags)
            .WithOne()
            .HasForeignKey(f => f.RecipeId);
    }
}
