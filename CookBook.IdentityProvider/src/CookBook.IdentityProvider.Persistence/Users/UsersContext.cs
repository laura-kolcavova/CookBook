using CookBook.IdentityProvider.Domain.Users;
using CookBook.IdentityProvider.Persistence.Shared.Interceptors;
using CookBook.IdentityProvider.Persistence.Users.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.IdentityProvider.Persistence.Users;

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
