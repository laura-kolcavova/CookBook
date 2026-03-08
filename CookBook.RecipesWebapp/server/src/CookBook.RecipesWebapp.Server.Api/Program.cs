using Carter;
using CookBook.RecipesWebapp.Server.Api.Shared.Extensions;
using CookBook.RecipesWebapp.Server.Api.Shared.ReverseProxy;
using CookBook.RecipesWebapp.Server.Api.Shared.SpaClient;
using CookBook.RecipesWebapp.Server.Application.Shared.Extensions;
using CookBook.RecipesWebapp.Server.Infrastructure.Shared.Configuration;
using CookBook.RecipesWebapp.Server.Infrastructure.Shared.Extensions;
using CookBook.RecipesWebapp.Server.Infrastructure.Shared.OpenIddict;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using OpenIddict.Abstractions;

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

var spaClientConfiguration = configuration
    .GetRequiredSection(nameof(SpaClientConfiguration))
    .Get<SpaClientConfiguration>()!;

var openIdConnectAppConfiguration = configuration
    .GetRequiredSection(nameof(OpenIdConnectAppConfiguration))
    .Get<OpenIdConnectAppConfiguration>()!;

var reverseProxyConfiguration = configuration
    .GetRequiredSection(ReverseProxyConstants.ConfigurationSectionName);

services
    .AddOptions();

services
    .AddAntiforgery(options =>
    {
        options.HeaderName = ConfigurationConstants.Antiforgery.HeaderName;
        options.Cookie.Name = ConfigurationConstants.Antiforgery.CookieName;
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    });

services
    .AddAuthentication(
        options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
        })
    .AddCookie(
        options =>
        {
            //options.LoginPath = "/login";
            //options.LogoutPath = "/logout";

            options.ExpireTimeSpan = TimeSpan.FromDays(1);
            options.SlidingExpiration = true;

            options.Cookie.Name = ConfigurationConstants.Identity.CookieName;
            options.Cookie.HttpOnly = true;
            options.Cookie.SameSite = SameSiteMode.Strict;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

        })
    .AddOpenIdConnect(
        options =>
        {
            options.Authority = openIdConnectAppConfiguration.Authority;
            options.ClientId = openIdConnectAppConfiguration.ClientId;
            options.ClientSecret = openIdConnectAppConfiguration.ClientSecret;

            options.ResponseType = OpenIdConnectResponseType.Code;
            options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            options.Scope.Add(OpenIddictConstants.Scopes.OpenId);
            options.Scope.Add(OpenIddictConstants.Scopes.Email);
            options.Scope.Add(OpenIddictConstants.Scopes.Profile);

            if (isDevelopment)
            {
                options.RequireHttpsMetadata = false;
            }

            options.SaveTokens = true;
            options.MapInboundClaims = false;
            options.GetClaimsFromUserInfoEndpoint = true;
        });

services
    .AddApplication()
    .AddInfrastructure()
    .AddApi(
        builder.Environment.ApplicationName,
        reverseProxyConfiguration)
    .AddSpaClient(spaClientConfiguration);

var app = builder.Build();

if (isDevelopment)
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapCarter();

app.MapReverseProxy();

if (spaClientConfiguration.IsSpaEnabled)
{
    app.MapWhen(ctx =>
        !ctx.Request.IsApiRequest() &&
        !ctx.Request.IsLessKnownRequest(),
        spaAppBuilder =>
        {
            spaAppBuilder.UseWhen(
                ctx => ctx.Request.IsRenderingRequest(),
                appBuilder =>
                {
                });

            var useStaticFiles = !spaClientConfiguration.UseDevelopmentProxyServer;

            if (useStaticFiles)
            {
                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(
                        Path.Combine(
                            Directory.GetCurrentDirectory(),
                            spaClientConfiguration.StaticFilesRootPath),
                        ExclusionFilters.None)
                });
            }

            spaAppBuilder.UseSpa(spa =>
            {
                if (spaClientConfiguration.UseDevelopmentProxyServer)
                {
                    spa.UseProxyToSpaDevelopmentServer(
                        spaClientConfiguration.DevelopmentProxyServerBaseUri);
                }
            });
        });
}

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
