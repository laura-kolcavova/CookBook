using CookBook.Extensions.AspNetCore.FluentValidation.EndpointFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CookBook.Extensions.AspNetCore.FluentValidation.Extensions;

public static class RouteBuilderExtensions
{
    public static RouteHandlerBuilder AddFluentValidation(
        this RouteHandlerBuilder builder)
    {
        return builder
            .AddEndpointFilter<FluentValidationEndpointFilter>();
    }

    public static RouteGroupBuilder AddFluentValidation(
       this RouteGroupBuilder builder)
    {
        return builder
            .AddEndpointFilter<FluentValidationEndpointFilter>();
    }
}
