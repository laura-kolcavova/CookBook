using CookBook.Extensions.AspNetCore.SqlServer.Extensions;
using CookBook.Recipes.Api.Shared;
using CookBook.Recipes.Api.Shared.Extensions;
using CookBook.Recipes.Application.Shared.Extensions;
using CookBook.Recipes.Infrastructure.Shared.Configuration;
using CookBook.Recipes.Infrastructure.Shared.Extensions;
using CookBook.Recipes.Infrastructure.Shared.OpenIdConnect;

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
    ConfigurationConstants.SqlConnectionStrings.CookBookRecipesSectionName);

var openIdConnectAppConfiguration = configuration
    .GetRequiredSection(nameof(OpenIdConnectAppConfiguration))
    .Get<OpenIdConnectAppConfiguration>()!;

services
    .AddOptions();

services
    .AddApplication()
    .AddInfrastructure(
        cookBookRecipesConnectionString,
        isDevelopment)
    .AddApi(
        builder.Environment.ApplicationName,
        isDevelopment,
        openIdConnectAppConfiguration);

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

app.UseAuthentication();
app.UseAuthorization();

app.MapApiEndpoints();

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

app.Run();
