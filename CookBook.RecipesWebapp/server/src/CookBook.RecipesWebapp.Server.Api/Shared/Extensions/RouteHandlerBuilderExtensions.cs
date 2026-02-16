using CookBook.Extensions.AspNetCore.FluentValidation;

namespace CookBook.RecipesWebapp.Server.Api.Shared.Extensions;

internal static class RouteHandlerBuilderExtensions
{
    // TODO move this into extensions project
    public static RouteHandlerBuilder WithFluentValidation(
        this RouteHandlerBuilder builder)
    {
        return builder
            .AddEndpointFilter<FluentValidationEndpointFilter>();
    }
}