using CookBook.Extensions.AspNetCore.SqlServer;
using CookBook.Recipes.Api.Shared;
using CookBook.Recipes.Api.Shared.Configuration;
using CookBook.Recipes.Api.Shared.Extensions;
using CookBook.Recipes.Application.Shared.Extensions;
using CookBook.Recipes.Persistence.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;
var isDevelopment = builder.Environment.IsDevelopment();

builder.Host
    .UseDefaultServiceProvider((context, options) =>
    {
        options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
        options.ValidateOnBuild = context.HostingEnvironment.IsDevelopment();
    });

var cookBookRecipesConnectionString = configuration.GetSqlConnectionString(
    ConfigurationConstants.CookBookRecipesConnectionStringSectionName);

services
    .AddOptions();

services
    .AddApplication()
    .AddPersistence(
        cookBookRecipesConnectionString,
        isDevelopment)
    .AddApi(
        builder.Environment.ApplicationName);

var app = builder.Build();

if (isDevelopment)
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler();
}

app.UseRouting();

//app.UseCors();
//app.UseAuthentication();
//app.UseAuthorization();

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

app
    .MapEndpoints();

app.Run();
