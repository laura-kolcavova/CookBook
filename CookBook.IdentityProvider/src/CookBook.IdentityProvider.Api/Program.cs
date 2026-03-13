using Carter;
using CookBook.Extensions.AspNetCore.SqlServer.Extensions;
using CookBook.IdentityProvider.Api.Shared.Extensions;
using CookBook.IdentityProvider.Application.Shared.Extensions;
using CookBook.IdentityProvider.Infrastructure.Shared.Configuration;
using CookBook.IdentityProvider.Infrastructure.Shared.Extensions;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

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

var cookBookIdentityProviderConnectionString = configuration.GetSqlConnectionString(
    ConfigurationConstants.SqlConnectionStrings.CookBookIdentityProviderSectionName);

services
    .AddOptions();

services
    .AddApplication()
    .AddInfrastructure(
        cookBookIdentityProviderConnectionString,
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

var supportedCultures = new List<CultureInfo>
{
    new CultureInfo( "en-GB" ),
};

app.UseRequestLocalization(
    new RequestLocalizationOptions
    {
        DefaultRequestCulture = new RequestCulture("en-GB"),
        SupportedCultures = supportedCultures,
        SupportedUICultures = supportedCultures
    });

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapCarter();

app.MapRazorPages();

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
