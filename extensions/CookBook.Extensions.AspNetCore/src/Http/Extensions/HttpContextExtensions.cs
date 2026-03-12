using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.RecipesWebapp.Server.Api.Shared.Extensions;

public static class HttpContextExtensions
{
    public static async Task WriteProblemDetails(
        this HttpContext httpContext,
        int statusCode)
    {
        var problemDetailsService = httpContext
            .RequestServices
            .GetRequiredService<IProblemDetailsService>();

        httpContext.Response.StatusCode = statusCode;

        var problemDetailsContext = new ProblemDetailsContext
        {
            HttpContext = httpContext
        };

        await problemDetailsService.WriteAsync(problemDetailsContext);
    }

    public static async Task WriteProblemDetails(
        this HttpContext httpContext,
        ProblemDetails problemDetails)
    {
        var problemDetailsService = httpContext
            .RequestServices
            .GetRequiredService<IProblemDetailsService>();

        httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status400BadRequest;

        var problemDetailsContext = new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails = problemDetails
        };

        await problemDetailsService.WriteAsync(problemDetailsContext);
    }
}
