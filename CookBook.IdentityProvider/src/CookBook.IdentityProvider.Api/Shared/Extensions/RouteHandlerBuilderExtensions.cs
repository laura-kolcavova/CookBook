using CookBook.Extensions.AspNetCore.Shared;

namespace CookBook.IdentityProvider.Api.Shared.Extensions;

internal static class RouteHandlerBuilderExtensions
{
    public static RouteHandlerBuilder HandleOperationCancelled(
        this RouteHandlerBuilder builder)
    {
        return builder
            .AddEndpointFilter<OperationCanceledExceptionFilter>();
    }
}
