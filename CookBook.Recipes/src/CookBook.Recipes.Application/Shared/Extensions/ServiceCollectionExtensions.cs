using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Recipes.Application.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services;
    }
}
