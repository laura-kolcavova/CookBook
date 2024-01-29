using CookBook.Recipes.Infrastructure.DatabaseContexts;
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

        //services.AddDbContext<BookCatalogContext>((_, options) =>
        //{
        //    options
        //        .UseSqlServer(connectionString)
        //        .EnableDetailedErrors(isDevelopment)
        //        .EnableSensitiveDataLogging(isDevelopment);
        //});

        return services;
    }
}
