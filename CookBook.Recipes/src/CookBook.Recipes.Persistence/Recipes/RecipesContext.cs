using CookBook.Recipes.Domain.Recipes;
using CookBook.Recipes.Persistence.Recipes.EntityTypeConfigurations;
using CookBook.Recipes.Persistence.Shared.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Persistence.Recipes;

internal sealed class RecipesContext : DbContext
{
    private readonly string _connectionString;

    private readonly bool _useDevelopmentLogging;

    private readonly UpdateTrackingFieldsInterceptor _updateTrackingFieldsInterceptor;

    public DbSet<RecipeAggregate> Recipes => Set<RecipeAggregate>();

    public RecipesContext(
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
        modelBuilder.ApplyConfiguration(new RecipeAggregateRootConfiguration());
        modelBuilder.ApplyConfiguration(new RecipeIngredientEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RecipeInstructionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RecipeTagEntityConfiguration());
    }

    private static ILoggerFactory CreateLoggerFactory()
    {
        return LoggerFactory.Create(builder =>
            builder.AddConsole());
    }
}
