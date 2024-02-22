using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CookBook.Extensions.AspNetCore.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInternalOrPublicValidators(this IServiceCollection services, Assembly assembly, ServiceLifetime serviceLifetime)
    {
        var types = assembly.GetTypes();

        new AssemblyScanner(types).ForEach(pair =>
        {
            services.Add(ServiceDescriptor.Describe(pair.InterfaceType, pair.ValidatorType, serviceLifetime));
        });

        return services;
    }
}
