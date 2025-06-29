﻿using CookBook.Recipes.Domain.Recipes.Entities;
using CookBook.Recipes.Persistence.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.Recipes.Persistence.Recipes.EntityTypeConfigurations;

internal sealed class RecipeTagEntityConfiguration :
    IEntityTypeConfiguration<RecipeTagEntity>
{
    public void Configure(
        EntityTypeBuilder<RecipeTagEntity> builder)
    {
        builder.ToTable(
            nameof(RecipesContext.RecipeTags),
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
