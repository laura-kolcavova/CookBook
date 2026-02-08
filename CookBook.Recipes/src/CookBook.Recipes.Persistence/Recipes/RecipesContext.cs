using CookBook.Recipes.Domain.Recipes;
using CookBook.Recipes.Persistence.Recipes.EntityTypeConfigurations;
using CookBook.Recipes.Persistence.Shared.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Persistence.Recipes;

internal sealed class RecipesContext(
    string connectionString,
    bool useDevelopmentLogging,
    UpdateTrackingFieldsInterceptor updateTrackingFieldsInterceptor) :
    DbContext
{
    public DbSet<RecipeAggregate> Recipes => Set<RecipeAggregate>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RecipeAggregateEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new RecipeIngredientEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new RecipeInstructionEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new RecipeTagEntityTypeConfiguration());
    }

    private static ILoggerFactory CreateLoggerFactory()
    {
        return LoggerFactory.Create(builder =>
            builder.AddConsole());
    }
}
