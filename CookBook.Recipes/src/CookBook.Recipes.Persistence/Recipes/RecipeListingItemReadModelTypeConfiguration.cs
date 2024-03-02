using CookBook.Recipes.Constants;
using CookBook.Recipes.Domain.Recipes;
using CookBook.Recipes.Persistence.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.Recipes.Persistence.Recipes;
internal class RecipeListingItemReadModelTypeConfiguration : IEntityTypeConfiguration<RecipeListingItemReadModel>
{
    public void Configure(EntityTypeBuilder<RecipeListingItemReadModel> builder)
    {
        builder
            .ToView(
            $"{DatabaseConfigurations.ViewPrefix}{nameof(RecipesReadContext.RecipeListingItemsView)}",
            RecipesReadContext.Schema);

        builder
            .HasKey(e => e.Id);

        builder
            .Property(e => e.Title)
            .IsRequired();
    }
}
