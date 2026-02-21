using CookBook.Recipes.Application.Recipes.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Recipes.Application.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services
            .AddRecipes();

        return services;
    }
}
