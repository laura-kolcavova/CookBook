using CookBook.Extensions.AspNetCore.FluentValidation;
using CookBook.Extensions.AspNetCore.Shared;

namespace CookBook.IdentityProvider.Api.Shared.Extensions;

internal static class RouteHandlerBuilderExtensions
{
    // TODO move this into extensions project
    public static RouteHandlerBuilder ValidateRequest(
        this RouteHandlerBuilder builder)
    {
        return builder
            .AddEndpointFilter<FluentValidationEndpointFilter>();
    }

    // TODO move this into extensions project
    public static RouteHandlerBuilder HandleOperationCancelled(
        this RouteHandlerBuilder builder)
    {
        return builder
            .AddEndpointFilter<OperationCanceledExceptionFilter>();
    }
}
