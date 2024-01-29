using CookBook.Extensions.AspNetCore.Filters;
using CookBook.Extensions.Configuration.SqlServer;
using CookBook.Recipes.Api.Configuration;
using CookBook.Recipes.Api.Endpoints;
using CookBook.Recipes.Api.Extensions;
using CookBook.Recipes.Persistence;
using Opw.HttpExceptions.AspNetCore;

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
    .InstallDbServices(cookBookRecipesConnectionString)
    .InstallSwaggerServices(builder.Environment.ApplicationName)
    .InstallApiServices()
    .InstallPersistenceServices(cookBookRecipesConnectionString, isDevelopment);

var app = builder.Build();

app.UseRouting();
app.UseHttpExceptions();

//app.UseAuthentication();
//app.UseAuthorization();

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
