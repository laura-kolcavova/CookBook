﻿using CookBook.Recipes.Domain.Recipes.Entities;
using CookBook.Recipes.Persistence.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.Recipes.Persistence.Recipes.EntityTypeConfigurations;

internal sealed class RecipeInstructionEntityConfiguration :
    IEntityTypeConfiguration<RecipeInstructionEntity>
{
    public void Configure(
        EntityTypeBuilder<RecipeInstructionEntity> builder)
    {
        builder.ToTable(
            nameof(RecipesContext.RecipeInstructions),
            DboSchema.Name);

        builder
            .HasKey(e => new
            {
                e.RecipeId,
                e.LocalId,
            });

        builder
            .Property(e => e.RecipeId)
            .IsRequired();

        builder
            .Property(e => e.Note)
            .HasMaxLength(1024)
            .IsRequired();

        builder
            .Property(e => e.OrderIndex)
            .HasDefaultValue(0)
            .IsRequired();
    }
}
