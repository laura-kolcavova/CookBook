using CookBook.Extensions.AspNetCore.Abort.EndpointFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CookBook.Extensions.AspNetCore.Abort.Extensions;

public static class RouteBuilderExtensions
{
    public static RouteHandlerBuilder AddClosedRequest(
       this RouteHandlerBuilder builder)
    {
        return builder
            .AddEndpointFilter<OperationCanceledExceptionFilter>();
    }

    public static RouteGroupBuilder AddClosedRequest(
       this RouteGroupBuilder builder)
    {
        return builder
            .AddEndpointFilter<OperationCanceledExceptionFilter>();
    }
}
