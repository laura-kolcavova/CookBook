using CookBook.Extensions.AspNetCore.EndpointFilters;
using CookBook.Extensions.Configuration.SqlServer;
using CookBook.Recipes.Api;
using CookBook.Recipes.Api.Shared.Configuration;
using CookBook.Recipes.Application;
using CookBook.Recipes.Persistence;
using System.Text.Json.Serialization;

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
var isDevelopment = builder.Environment.IsDevelopment();

var cookBookRecipesConnectionString = configuration
    .GetSqlConnectionString(ConfigurationConstants.CookBookRecipesConnectionStringSectionName);

services
    .AddOptions();

services
    .ConfigureHttpJsonOptions(options =>
    {
        options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

services
    .InstallDbServices(cookBookRecipesConnectionString)
    .InstallApiServices(builder.Environment.ApplicationName)
    .InstallPersistenceServices(cookBookRecipesConnectionString, isDevelopment)
    .InstallApplicationServices();

var app = builder.Build();

app.UseRouting();


if (!isDevelopment)
{
    app.UseExceptionHandler();
}

//app.UseCors();
//app.UseAuthentication();
//app.UseAuthorization();

if (isDevelopment)
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
        options.ConfigObject.Filter = string.Empty;
        options.ConfigObject.TryItOutEnabled = true;
    });
}

app
    .AddEndpoints()
    .AddEndpointFilter<OperationCanceledExceptionFilter>()
    .AddEndpointFilter<FluentValidationEndpointFilter>()
    .WithOpenApi();

app.Run();
