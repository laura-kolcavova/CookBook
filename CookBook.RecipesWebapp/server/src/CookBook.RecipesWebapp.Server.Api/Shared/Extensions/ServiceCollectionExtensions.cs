using Carter;
using FluentValidation;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json.Serialization;

namespace CookBook.RecipesWebapp.Server.Api.Shared.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApi(
        this IServiceCollection services,
        string applicationName,
        IConfigurationSection reverseProxyOptions)
    {
        services
            .AddCarter(new DependencyContextAssemblyCatalog([typeof(Program).Assembly]));

        services
            .ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        services
            .AddEndpointsApiExplorer()
            .AddSwaggerExamplesFromAssemblyOf<Program>()
            .AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = applicationName,
                    Version = "v1"
                });

                options.ExampleFilters();
                options.SupportNonNullableReferenceTypes();

                options.CustomSchemaIds(x => x.FullName?
                    .Replace("Dto", string.Empty)
                    .Replace("+", "."));
            });

        services
            .AddReverseProxy()
            .LoadFromConfig(reverseProxyOptions);

        services
            .AddProblemDetails();

        services
            .AddValidatorsFromAssembly(
                typeof(Program).Assembly,
                ServiceLifetime.Singleton,
                includeInternalTypes: true);

        return services;
    }
}
