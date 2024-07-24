using Microsoft.OpenApi.Models;

namespace CookBook.RecipesWebapp.Server.Api;

internal static class ApiServiceInstallation
{
    public static IServiceCollection InstallApiServices(this IServiceCollection services, string applicationName)
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

        //services
        //    .AddControllers()
        //    .AddJsonOptions(options =>
        //    {
        //        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        //    });

        services
            .AddProblemDetails();

        return services;
    }
}
