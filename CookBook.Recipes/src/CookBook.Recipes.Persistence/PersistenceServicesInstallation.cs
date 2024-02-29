using CookBook.Recipes.Application.Features;
using CookBook.Recipes.Domain.Recipes;
using CookBook.Recipes.Infrastructure.DatabaseContexts;
using CookBook.Recipes.Persistence.Common;
using CookBook.Recipes.Persistence.DatabaseContexts;
using CookBook.Recipes.Persistence.Recipes;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Recipes.Persistence;

public static class PersistenceServicesInstallation
{
    public static IServiceCollection InstallPersistenceServices(
        this IServiceCollection services,
        string connectionString,
        bool isDevelopment)
    {
        services.AddScoped(_ => new RecipesContext(
            connectionString: connectionString,
            useDevelopmentLogging: isDevelopment));

        services.AddScoped(_ => new RecipesReadContext(
            connectionString: connectionString,
            useDevelopmentLogging: isDevelopment));

        //services.AddDbContext<BookCatalogContext>((_, options) =>
        //{
        //    options
        //        .UseSqlServer(connectionString)
        //        .EnableDetailedErrors(isDevelopment)
        //        .EnableSensitiveDataLogging(isDevelopment);
        //});

        services
            .AddScoped<IRecipeRepository, RecipeRepository>();

        services
            .AddScoped(
            typeof(IRecipeListingItemsRepository),
            typeof(EntityFrameworkReadModelRepository<RecipesReadContext, RecipeListingItemReadModel, long>));

        return services;
    }
}
