using CookBook.IdentityProvider.Domain.Users;
using CookBook.IdentityProvider.Infrastructure.Shared.Interceptors;
using CookBook.IdentityProvider.Infrastructure.Users.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.IdentityProvider.Infrastructure.Users;

internal sealed class UsersContext(
    string connectionString,
    bool useDevelopmentLogging,
    UpdateTrackingFieldsInterceptor updateTrackingFieldsInterceptor) :
    DbContext
{
    public DbSet<UserAggregate> Users => Set<UserAggregate>();

    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
           .UseSqlServer(connectionString)
           .AddInterceptors(updateTrackingFieldsInterceptor);

        if (useDevelopmentLogging)
        {
            optionsBuilder
                .UseLoggerFactory(CreateLoggerFactory())
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging();
        }
    }

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserAggregateEntityTypeConfiguration());
    }

    private static ILoggerFactory CreateLoggerFactory()
    {
        return LoggerFactory.Create(builder =>
            builder.AddConsole());
    }
}
