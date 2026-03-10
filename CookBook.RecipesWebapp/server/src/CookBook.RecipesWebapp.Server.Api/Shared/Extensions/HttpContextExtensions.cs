namespace CookBook.RecipesWebapp.Server.Api.Shared.Extensions;

internal static class HttpContextExtensions
{
    public static async Task WriteProblemDetails(
        this HttpContext httpContext,
        int statusCode)
    {
        var problemDetailsService = httpContext.RequestServices
            .GetRequiredService<IProblemDetailsService>();

        httpContext.Response.StatusCode = statusCode;

        var problemDetailsContext = new ProblemDetailsContext
        {
            HttpContext = httpContext
        };

        await problemDetailsService.WriteAsync(problemDetailsContext);
    }
}
