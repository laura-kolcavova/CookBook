using Carter;
using CookBook.Extensions.AspNetCore.SqlServer;
using CookBook.IdentityProvider.Api.Shared.Configuration;
using CookBook.IdentityProvider.Api.Shared.Extensions;
using CookBook.IdentityProvider.Domain.Shared.Extensions;
using CookBook.IdentityProvider.Persistence.Shared.Extensions;

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

var cookBookUsersConnectionString = configuration.GetSqlConnectionString(
    ConfigurationConstants.CookBookUsersConnectionStringSectionName);

services
    .AddOptions();

services
    .AddDomain()
    .AddPersistence(
        cookBookUsersConnectionString,
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

app.MapCarter()

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