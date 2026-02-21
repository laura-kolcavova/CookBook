using CookBook.IdentityProvider.Domain.Users;
using CookBook.IdentityProvider.Infrastructure.Shared.Constants;
using CookBook.IdentityProvider.Infrastructure.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CookBook.IdentityProvider.Infrastructure.Users.EntityTypeConfigurations;

internal sealed class UserAggregateEntityTypeConfiguration :
    IEntityTypeConfiguration<UserAggregate>
{
    public void Configure(
        EntityTypeBuilder<UserAggregate> builder)
    {
        builder.ToTable(
           DboSchema.UsersTableName,
           DboSchema.Name);

        builder
            .HasKey(e => e.Id);

        builder
            .Property(e => e.Id)
            .IsRequired();

        builder
           .Property(e => e.IdentityUserId)
           .IsRequired();

        builder
           .Property(e => e.UserNumber)
           .IsRequired();

        builder
            .Property(e => e.DisplayName)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .HasTrackableProperties();
    }
}
