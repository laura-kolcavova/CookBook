using CookBook.Recipes.Domain.Recipes.Entities;
using CookBook.Recipes.Infrastructure.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.Recipes.Infrastructure.Recipes.EntityTypeConfigurations;

internal sealed class RecipeTagEntityTypeConfiguration :
    IEntityTypeConfiguration<RecipeTagEntity>
{
    public void Configure(
        EntityTypeBuilder<RecipeTagEntity> builder)
    {
        builder.ToTable(
            DboSchema.RecipeTagsTableName,
            DboSchema.Name);

        builder
            .HasKey(e => new
            {
                e.RecipeId,
                e.Name,
            });

        builder
            .Property(e => e.RecipeId)
            .IsRequired();

        builder
            .Property(e => e.Name)
            .HasMaxLength(256)
            .IsRequired();
    }
}
