using CookBook.Recipes.Domain.Recipes;
using CookBook.Recipes.Persistence.Recipes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Persistence.DatabaseContexts;

internal class RecipesReadContext : DbContext
{
    public const string Schema = "dbo";

    public DbSet<RecipeListingItemReadModel> RecipeListingItemsView => Set<RecipeListingItemReadModel>();

    private readonly string _connectionString;

    private readonly bool _useDevelopmentLogging;

    public RecipesReadContext(
       string connectionString,
       bool useDevelopmentLogging)
    {
        _connectionString = connectionString;
        _useDevelopmentLogging = useDevelopmentLogging;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
           .UseSqlServer(_connectionString);

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
        modelBuilder.ApplyConfiguration(new RecipeListingItemReadModelTypeConfiguration());
    }

    private static ILoggerFactory CreateLoggerFactory()
    {
        return LoggerFactory.Create(builder =>
            builder.AddConsole());
    }
}
