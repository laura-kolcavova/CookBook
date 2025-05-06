using CookBook.Recipes.Domain.Categories;
using CookBook.Recipes.Persistence.Categories.EntityTypeConfigurations;
using CookBook.Recipes.Persistence.Shared.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Persistence.Shared.DatabaseContexts;

internal sealed class CategoriesContext : DbContext
{
    private readonly string _connectionString;

    private readonly bool _useDevelopmentLogging;

    private readonly UpdateTrackingFieldsInterceptor _updateTrackingFieldsInterceptor;

    #region AggregateRoots

    public DbSet<CategoryAggregate> Categories => Set<CategoryAggregate>();

    #endregion AggregateRoots

    public CategoriesContext(
        string connectionString,
        bool useDevelopmentLogging,
        UpdateTrackingFieldsInterceptor updateTrackingFieldsInterceptor)
    {
        _connectionString = connectionString;
        _useDevelopmentLogging = useDevelopmentLogging;
        _updateTrackingFieldsInterceptor = updateTrackingFieldsInterceptor;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
           .UseSqlServer(_connectionString)
           .AddInterceptors(_updateTrackingFieldsInterceptor);

        if (_useDevelopmentLogging)
        {
            optionsBuilder
                .UseLoggerFactory(CreateLoggerFactory())
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryAggregateRootConfiguration());
    }

    private static ILoggerFactory CreateLoggerFactory()
    {
        return LoggerFactory.Create(builder =>
            builder.AddConsole());
    }
}
