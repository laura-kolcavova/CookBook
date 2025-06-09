using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CookBook.Extensions.AspNetCore.FluentValidation;

public class FluentValidationEndpointFilter : IEndpointFilter
{
    private readonly IServiceProvider _serviceProvider;

    public FluentValidationEndpointFilter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        foreach (var argument in context.Arguments)
        {
            if (argument is null)
            {
                continue;
            }

            var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());
            var validator = _serviceProvider.GetService(validatorType) as IValidator;

            if (validator is null)
            {
                continue;
            }

            var validationContextType = typeof(ValidationContext<>).MakeGenericType(argument.GetType());
            var validationContext = (IValidationContext)Activator.CreateInstance(validationContextType, argument)!;

            var validationResult = await validator.ValidateAsync(validationContext, context.HttpContext.RequestAborted);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(
                    validationResult.Errors.ToDictionary(),
                    //detail: "Please refer to the errors property for additional details",
                    instance: context.HttpContext.Request.Path,
                    title: "One or more validation errors occurred.",
                    type: "https://tools.ietf.org/html/rfc7231#section-6.5.1");
            }
        }

        return await next.Invoke(context);
    }
}
