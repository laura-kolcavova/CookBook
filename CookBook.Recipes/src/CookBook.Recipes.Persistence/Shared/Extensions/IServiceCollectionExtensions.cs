using CookBook.Recipes.Application.Categories.Services;
using CookBook.Recipes.Application.Recipes.Services;
using CookBook.Recipes.Persistence.Categories.Services;
using CookBook.Recipes.Persistence.Recipes.Services;
using CookBook.Recipes.Persistence.Shared.DatabaseContexts;
using CookBook.Recipes.Persistence.Shared.Interceptors;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Recipes.Persistence.Shared.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services,
        string connectionString,
        bool isDevelopment)
    {
        services
            .AddRecipes()
            .AddCategories();

        services
            .AddSingleton<UpdateTrackingFieldsInterceptor>();

        services
            .AddScoped(serviceProvider =>
            {
                var updateTrackingFieldsInterceptor = serviceProvider
                    .GetRequiredService<UpdateTrackingFieldsInterceptor>();

                return new RecipesContext(
                    connectionString: connectionString,
                    useDevelopmentLogging: isDevelopment,
                    updateTrackingFieldsInterceptor: updateTrackingFieldsInterceptor);
            });

        services
            .AddScoped(serviceProvider =>
            {
                var updateTrackingFieldsInterceptor = serviceProvider
                    .GetRequiredService<UpdateTrackingFieldsInterceptor>();

                return new CategoriesContext(
                    connectionString: connectionString,
                    useDevelopmentLogging: isDevelopment,
                    updateTrackingFieldsInterceptor: updateTrackingFieldsInterceptor);
            });

        return services;
    }

    public static IServiceCollection AddRecipes(
        this IServiceCollection services)
    {
        services
            .AddScoped<ISaveRecipeService, SaveRecipeService>()
            .AddScoped<IRemoveRecipeService, RemoveRecipeService>()
            .AddScoped<ISearchRecipesService, SearchRecipesService>()
            .AddScoped<IGetRecipeDetailService, GetRecipesDetailService>();

        return services;
    }

    public static IServiceCollection AddCategories(
        this IServiceCollection services)
    {
        services
            .AddScoped<IAddCategoryService, AddCategoryService>()
            .AddScoped<IMoveCategoryService, MoveCategoryService>()
            .AddScoped<IRemoveCategoryService, RemoveCategoryService>()
            .AddScoped<IRenameCategoryService, RenameCategoryService>()
            .AddScoped<IGetCategoriesService, GetCategoriesService>()
            .AddScoped<IGetCategoryDetailService, GetCategoryDetailService>();

        return services;
    }
}
