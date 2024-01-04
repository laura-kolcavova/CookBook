using CookBook.Extensions.AspNetCore.Filters;
using CookBook.Extensions.Configuration.SqlServer;
using CookBook.Recipes.Api.Configuration;
using CookBook.Recipes.Api.Endpoints;
using Microsoft.OpenApi.Models;
using Opw.HttpExceptions.AspNetCore;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseDefaultServiceProvider((context, options) =>
    {
        options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
        options.ValidateOnBuild = context.HostingEnvironment.IsDevelopment();
    });

// Add services to the container.
var services = builder.Services;
var configuration = builder.Configuration;

var cookBookRecipesConnectionString = configuration
    .GetSqlConnectionString(ConfigurationConstants.CookBookRecipesConnectionStringSectionName);

//services
//    .AddAuthentication(options =>
//    {
//        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    })
//    .AddJwtBearer(options => builder.Configuration.GetSection(nameof(JwtBearerOptions)).Bind(options));

services
    .AddOptions();

services
    .AddHealthChecks()
    .AddSqlServer(cookBookRecipesConnectionString, name: "CookBookRecipes_DB", tags: new[] { "readiness" });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services
    .AddEndpointsApiExplorer()
    .AddSwaggerExamplesFromAssemblyOf<Program>()
    .AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = builder.Environment.ApplicationName, Version = "v1" });
        options.ExampleFilters();
        options.CustomSchemaIds(x => x.FullName?.Replace("Dto", string.Empty));

        // Set the comments path for the Swagger JSON and UI.
        var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "CookBook.Recipes.*.xml", SearchOption.TopDirectoryOnly).ToList();
        xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));
    });

//services
//.AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
//})
//.AddHttpExceptions(options =>
//{
//    // Only log the when it has a status code of 500 or higher, or when it not is a HttpException.
//    options.ShouldLogException = exception =>
//    {
//        return (exception is HttpExceptionBase httpException &&
//            (int)httpException.StatusCode >= 500) ||
//            exception is not HttpExceptionBase;
//    };
//});


var app = builder.Build();

app.UseRouting();
app.UseHttpExceptions();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = ".less-known/api-docs/{documentName}.json";
        options.PreSerializeFilters.Add((swagger, _) =>
            // Clear servers -element in swagger.json because it got the wrong port when hosted behind reverse proxy
            swagger.Servers.Clear());

    });

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/.less-known/api-docs/v1.json", "v1");
        options.RoutePrefix = ".less-known/api-docs/ui";
    });
}

app
    .AddEndpoints()
    .AddEndpointFilter<OperationCanceledExceptionFilter>()
    .WithOpenApi();

app.Run();

