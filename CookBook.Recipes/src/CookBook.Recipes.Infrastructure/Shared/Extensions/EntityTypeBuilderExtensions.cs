using CookBook.Recipes.Domain.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.Recipes.Infrastructure.Shared.Extensions;

internal static class EntityTypeBuilderExtensions
{
    public static EntityTypeBuilder<TEntity> HasTrackableProperties<TEntity>(
        this EntityTypeBuilder<TEntity> builder)
        where TEntity : class, ITrackableEntity
    {
        builder
            .Property(o => o.CreatedAt)
            .HasColumnName("DateCreatedAt");

        builder
            .Property(c => c.UpdatedAt)
            .HasColumnName("DateUpdatedAt");

        return builder;
    }
}
