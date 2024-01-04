using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CookBook.Extensions.AspNetCore.Filters;

public class OperationCanceledExceptionFilter : IEndpointFilter
{
    private readonly ILogger<OperationCanceledExceptionFilter> _logger;

    public OperationCanceledExceptionFilter(
       ILogger<OperationCanceledExceptionFilter> logger)
    {
        _logger = logger;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        try
        {
            return await next(context);
        }
        catch (OperationCanceledException ex)
        {
            _logger.LogDebug(ex, "Request was cancelled");

            // Return 499 Client Closed Request
            // https://httpstatuses.com/499
            return Results.StatusCode(499);
        }
    }
}
