using CookBook.RecipesWebapp.Server.Api.Shared.Antiforgery.EndpointFilters;

namespace CookBook.RecipesWebapp.Server.Api.Shared.Antiforgery.Extensions;

internal static class RouteBuilderExtensions
{
    public static RouteHandlerBuilder AddAntiforgeryValidation(
      this RouteHandlerBuilder builder)
    {
        return builder
            .AddEndpointFilter<AntiforgeryValidationFilter>();
    }

    public static RouteGroupBuilder AddAntiforgeryValidation(
       this RouteGroupBuilder builder)
    {
        return builder
            .AddEndpointFilter<AntiforgeryValidationFilter>();
    }
}
