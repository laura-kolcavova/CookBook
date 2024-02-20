using CookBook.Recipes.Domain.Entities.Recipes;
using CookBook.Recipes.Infrastructure.Common;
using CookBook.Recipes.Persistence.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CookBook.Recipes.Infrastructure.DatabaseContexts;

internal class RecipesContext : TrackableDbContext
{
    public const string Schema = "dbo";

    public DbSet<RecipeAggregate> Recipes => Set<RecipeAggregate>();

    public DbSet<RecipeIngredientEntity> RecipeIngredients => Set<RecipeIngredientEntity>();

    public DbSet<RecipeInstructionEntity> RecipeInstructions => Set<RecipeInstructionEntity>();

    private readonly string _connectionString;

    private readonly bool _useDevelopmentLogging;

    public RecipesContext(
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
        modelBuilder.ApplyConfiguration(new RecipeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new RecipeIngredientEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new RecipeInstructionEntityTypeConfiguration());
    }

    private static ILoggerFactory CreateLoggerFactory()
    {
        return LoggerFactory.Create(builder =>
            builder.AddConsole());
    }
}
