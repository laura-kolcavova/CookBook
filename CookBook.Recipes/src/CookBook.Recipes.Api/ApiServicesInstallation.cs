using Microsoft.OpenApi.Models;
using Opw.HttpExceptions;
using Opw.HttpExceptions.AspNetCore;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json.Serialization;

namespace CookBook.Recipes.Api;

internal static class ApiServicesInstallation
{
    public static IServiceCollection InstallApiServices(this IServiceCollection services, string applicationName)
    {
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerExamplesFromAssemblyOf<Program>()
            .AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = applicationName, Version = "v1" });
                options.ExampleFilters();
                options.CustomSchemaIds(x => x.FullName?.Replace("Dto", string.Empty));

                // Set the comments path for the Swagger JSON and UI.
                var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "CookBook.Recipes.*.xml", SearchOption.TopDirectoryOnly).ToList();
                xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));
            });

        //services
        //    .AddAuthentication(options =>
        //    {
        //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    })
        //    .AddJwtBearer(options => builder.Configuration.GetSection(nameof(JwtBearerOptions)).Bind(options));

        //services.AddAuthorizationBuilder()
        //    .AddPolicy("", policy =>
        //    {
        //        policy.RequireClaim("scope", "");
        //    })
        //    .AddClientsRightAsPolicy();

        services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            })
            .AddHttpExceptions(options =>
            {
                // Only log the when it has a status code of 500 or higher, or when it not is a HttpException.
                options.ShouldLogException = exception =>
                {
                    return exception is HttpExceptionBase httpException &&
                        (int)httpException.StatusCode >= 500 ||
                        exception is not HttpExceptionBase;
                };
            });

        services
            .AddProblemDetails();

        return services;
    }

    public static IServiceCollection InstallDbServices(this IServiceCollection services, string cookBookRecipesConnectionString)
    {
        services
            .AddHealthChecks()
            .AddSqlServer(cookBookRecipesConnectionString, name: "CookBookRecipes_DB", tags: new[] { "readiness" });

        return services;
    }
}
