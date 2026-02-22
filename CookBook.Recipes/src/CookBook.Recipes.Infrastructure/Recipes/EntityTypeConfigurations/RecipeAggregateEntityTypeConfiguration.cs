using CookBook.Recipes.Domain.Recipes;
using CookBook.Recipes.Infrastructure.Shared.Constants;
using CookBook.Recipes.Infrastructure.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.Recipes.Infrastructure.Recipes.EntityTypeConfigurations;

internal sealed class RecipeAggregateEntityTypeConfiguration :
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
            .HasColumnType("BIGINT")
            .UseIdentityColumn(1, 1)
            .IsRequired();

        builder
            .Property(e => e.UserName)
            .HasColumnType("NVARCHAR(256)")
            .HasMaxLength(256)
            .IsRequired();

        builder
            .Property(e => e.Title)
            .HasColumnType("NVARCHAR(256)")
            .HasMaxLength(256)
            .IsRequired();

        builder
            .Property(e => e.Description)
            .HasColumnType("NVARCHAR(1024)")
            .HasMaxLength(1024);

        builder
            .Property(e => e.Notes)
            .HasColumnType("NVARCHAR(1024)")
            .HasMaxLength(1024);

        builder
            .Property(e => e.Servings)
            .HasColumnType("SMALLINT")
            .HasDefaultValue(0)
            .IsRequired();

        builder
            .Property(e => e.CookTime)
            .HasColumnType("SMALLINT")
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
