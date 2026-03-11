
using Microsoft.AspNetCore.Antiforgery;

namespace CookBook.RecipesWebapp.Server.Api.Shared.Antiforgery.EndpointFilters;

internal sealed class AntiforgeryValidationFilter :
    IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var httpContext = context.HttpContext;

        if (
            httpContext.Request.Method == HttpMethods.Get ||
            httpContext.Request.Method == HttpMethods.Head ||
            httpContext.Request.Method == HttpMethods.Options)
        {
            return await next(context);
        }

        var antiforgery = httpContext
           .RequestServices
           .GetRequiredService<IAntiforgery>();

        try
        {
            await antiforgery.ValidateRequestAsync(httpContext);
        }
        catch (AntiforgeryValidationException)
        {
            return Results.Problem(
                detail: "Invalid or missing antiforgery token",
                statusCode: StatusCodes.Status400BadRequest);
        }

        return await next(context);
    }
}
