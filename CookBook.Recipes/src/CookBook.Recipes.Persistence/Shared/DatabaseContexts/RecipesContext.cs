using CookBook.Recipes.Domain.Categories;
using CookBook.Recipes.Domain.Recipes;
using CookBook.Recipes.Domain.Recipes.Entities;
using CookBook.Recipes.Persistence.Categories.EntityTypeConfigurations;
using CookBook.Recipes.Persistence.Recipes.EntityTypeConfigurations;
using CookBook.Recipes.Persistence.Shared.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Persistence.Shared.DatabaseContexts;

internal sealed class RecipesContext : DbContext
{
    public const string Schema = "dbo";

    private readonly string _connectionString;

    private readonly bool _useDevelopmentLogging;

    private readonly UpdateTrackingFieldsInterceptor _updateTrackingFieldsInterceptor;

    public RecipesContext(
        string connectionString,
        bool useDevelopmentLogging,
        UpdateTrackingFieldsInterceptor updateTrackingFieldsInterceptor)
    {
        _connectionString = connectionString;
        _useDevelopmentLogging = useDevelopmentLogging;
        _updateTrackingFieldsInterceptor = updateTrackingFieldsInterceptor;
    }

    #region AggregateRoots

    public DbSet<CategoryAggregate> Categories => Set<CategoryAggregate>();

    public DbSet<RecipeAggregate> Recipes => Set<RecipeAggregate>();

    #endregion AggregateRoots

    #region Entities

    public DbSet<RecipeIngredientEntity> RecipeIngredients => Set<RecipeIngredientEntity>();

    public DbSet<RecipeInstructionEntity> RecipeInstructions => Set<RecipeInstructionEntity>();

    public DbSet<RecipeCategoryEntity> RecipeCategories => Set<RecipeCategoryEntity>();

    public DbSet<RecipeTagEntity> RecipeTags => Set<RecipeTagEntity>();

    #endregion Entities

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

        modelBuilder.ApplyConfiguration(new RecipeAggregateRootConfiguration());
        modelBuilder.ApplyConfiguration(new RecipeIngredientEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RecipeInstructionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RecipeCategoryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RecipeTagEntityConfiguration());
    }

    private static ILoggerFactory CreateLoggerFactory()
    {
        return LoggerFactory.Create(builder =>
            builder.AddConsole());
    }
}
