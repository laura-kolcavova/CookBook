using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace CookBook.Extensions.AspNetCore.FluentValidation;

internal static class RouteHandlerBuilderExtensions
{
    public static RouteHandlerBuilder WithFluentValidation(
        this RouteHandlerBuilder builder)
    {
        return builder
            .AddEndpointFilter<FluentValidationEndpointFilter>();
    }
}
