using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CookBook.Recipes.Application;

public static class ApplicationServicesInstallation
{
    public static IServiceCollection InstallApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}
