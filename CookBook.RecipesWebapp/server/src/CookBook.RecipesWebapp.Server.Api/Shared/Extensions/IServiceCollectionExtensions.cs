using CookBook.RecipesWebapp.Server.Api.Shared.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace CookBook.RecipesWebapp.Server.Api.Shared.Extensions;

internal static class IServiceCollectionExtensions
{
    public static IServiceCollection AddClient(
       this IServiceCollection services,
       ClientOptions clientOptions)
    {
        // In production, the React files will be served from this directory
        services.AddSpaStaticFiles(c =>
        {
            c.RootPath = clientOptions.StaticFilesRootPath;
        });

        return services;
    }

    public static IServiceCollection AddApi(
        this IServiceCollection services,
        string applicationName,
        IConfigurationSection reverseProxyOptions)
    {
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

        return services;
    }
}
