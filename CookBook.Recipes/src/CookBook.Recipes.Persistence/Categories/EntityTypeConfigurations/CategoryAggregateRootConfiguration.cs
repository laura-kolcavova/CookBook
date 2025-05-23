﻿using CookBook.Recipes.Domain.Categories;
using CookBook.Recipes.Persistence.Shared.Constants;
using CookBook.Recipes.Persistence.Shared.DatabaseContexts;
using CookBook.Recipes.Persistence.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.Recipes.Persistence.Categories.EntityTypeConfigurations;

internal sealed class CategoryAggregateRootConfiguration : IEntityTypeConfiguration<CategoryAggregate>
{
    public void Configure(EntityTypeBuilder<CategoryAggregate> builder)
    {
        builder
            .ToTable(nameof(CategoriesContext.Categories), Schemas.Main);

        builder
            .HasKey(e => e.Id);

        builder
            .Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(256);

        builder
            .Property(e => e.ParentCategoryId)
            .IsRequired()
            .HasDefaultValue(0);

        builder
            .HasTrackableProperties();

        builder
            .HasMany(e => e.RecipeCategories)
            .WithOne()
            .HasForeignKey(f => f.CategoryId);
    }
}
