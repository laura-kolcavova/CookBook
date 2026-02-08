using CookBook.IdentityProvider.Domain.Shared.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.IdentityProvider.Persistence.Shared.Extensions;

internal static class EntityTypeBuilderExtensions
{
    public static EntityTypeBuilder<TEntity> HasTrackableProperties<TEntity>(
        this EntityTypeBuilder<TEntity> builder)
        where TEntity : class, ITrackableEntity
    {
        builder
            .Property(o => o.CreatedAt)
            .IsRequired();

        builder
            .Property(c => c.UpdatedAt);

        return builder;
    }
}
