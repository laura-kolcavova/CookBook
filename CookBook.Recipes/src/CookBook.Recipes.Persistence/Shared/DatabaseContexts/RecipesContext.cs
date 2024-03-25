using CookBook.Recipes.Domain.Categories;
using CookBook.Recipes.Domain.Recipes;
using CookBook.Recipes.Infrastructure.Common;
using CookBook.Recipes.Persistence.Categories.EntityTypeConfigurations;
using CookBook.Recipes.Persistence.Recipes.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Persistence.Shared.DatabaseContexts;

internal sealed class RecipesContext : TrackableDbContext
{
    public const string Schema = "dbo";

    private readonly string _connectionString;

    private readonly bool _useDevelopmentLogging;

    public RecipesContext(
        string connectionString,
        bool useDevelopmentLogging)
    {
        _connectionString = connectionString;
        _useDevelopmentLogging = useDevelopmentLogging;
    }

    #region AggregateRoots

    public DbSet<CategoryAggregate> Categories => Set<CategoryAggregate>();

    public DbSet<RecipeAggregate> Recipes => Set<RecipeAggregate>();

    #endregion AggregateRoots

    #region Entities

    public DbSet<RecipeIngredientEntity> RecipeIngredients => Set<RecipeIngredientEntity>();

    public DbSet<RecipeInstructionEntity> RecipeInstructions => Set<RecipeInstructionEntity>();

    #endregion Entities

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
        modelBuilder.ApplyConfiguration(new CategoryAggregateRootConfiguration());

        modelBuilder.ApplyConfiguration(new RecipeAggregateRootConfiguration());
        modelBuilder.ApplyConfiguration(new RecipeIngredientEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RecipeInstructionEntityConfiguration());
    }

    private static ILoggerFactory CreateLoggerFactory()
    {
        return LoggerFactory.Create(builder =>
            builder.AddConsole());
    }
}
