using CookBook.Extensions.AspNetCore.FluentValidation;

namespace CookBook.IdentityProvider.Api.Shared.Extensions;

internal static class RouteHandlerBuilderExtensions
{
    // TODO this is in extensions project
    public static RouteHandlerBuilder WithFluentValidation(
        this RouteHandlerBuilder builder)
    {
        return builder
            .AddEndpointFilter<FluentValidationEndpointFilter>();
    }
}
