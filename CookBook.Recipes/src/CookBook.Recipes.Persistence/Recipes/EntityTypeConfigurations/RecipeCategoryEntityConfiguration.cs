﻿using CookBook.Recipes.Domain.Recipes.Entities;
using CookBook.Recipes.Persistence.Shared.Constants;
using CookBook.Recipes.Persistence.Shared.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.Recipes.Persistence.Recipes.EntityTypeConfigurations;

internal class RecipeCategoryEntityConfiguration
    : IEntityTypeConfiguration<RecipeCategoryEntity>
{
    public void Configure(EntityTypeBuilder<RecipeCategoryEntity> builder)
    {
        builder
            .ToTable(nameof(RecipesContext.RecipeCategories), Schemas.Main);

        builder
            .HasKey(e => new
            {
                e.RecipeId,
                e.CategoryId
            });

        builder
            .Property(e => e.RecipeId)
            .IsRequired();

        builder
            .Property(e => e.CategoryId)
            .IsRequired();
    }
}
