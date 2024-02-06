using CookBook.Extensions.CSharpExtended.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel;

namespace CookBook.Extensions.AspNetCore.Utilities;

public static class EndpointResults
{
    public static Results<ProblemHttpResult, ValidationProblem>
        Problem(IEnumerable<ExpectedError> errors, HttpContext httpContext)
    {
        if (errors.Any())
        {
            throw new ArgumentException("A collection of errors cannot be empty", nameof(errors));
        }

        if (errors.All(error => error.Type == ExpectedErrorType.Validation))
        {
            return ValidationProblem(errors, httpContext);
        }

        var error = errors.First();

        return Problem(error, httpContext);
    }

    public static ProblemHttpResult Problem(ExpectedError error, HttpContext httpContext)
    {
        var extensions = new Dictionary<string, object?>
        {
            ["errorCode"] = error.Code
        };

        int statusCode;
        string title;
        string type;

        switch (error.Type)
        {
            case ExpectedErrorType.Failure:
                statusCode = StatusCodes.Status422UnprocessableEntity;
                title = "A failure error has occurred";
                type = "https://httpwg.org/specs/rfc9110.html#status.422";
                break;
            case ExpectedErrorType.NotFound:
                statusCode = StatusCodes.Status422UnprocessableEntity;
                title = "A 'Not found' error has occurred";
                type = "https://httpwg.org/specs/rfc9110.html#status.422";
                break;
            case ExpectedErrorType.Unauthorized:
                statusCode = StatusCodes.Status401Unauthorized;
                title = "An 'Unauthorized' error has occurred";
                type = "https://httpwg.org/specs/rfc9110.html#status.401";
                break;
            case ExpectedErrorType.NotEnoughRights:
                statusCode = StatusCodes.Status403Forbidden;
                title = "A 'Not enough rights' error has occurred";
                type = "https://httpwg.org/specs/rfc9110.html#status.403";
                break;
            default:
                throw new InvalidEnumArgumentException(nameof(error.Type));
        }

        return TypedResults.Problem(
                detail: error.Message,
                instance: httpContext.Request.Path,
                statusCode: statusCode,
                title: title,
                type: type,
                extensions: extensions);
    }

    public static ValidationProblem ValidationProblem(IEnumerable<ExpectedError> errors, HttpContext httpContext)
    {
        var validationErrors = new Dictionary<string, List<string>>();

        foreach (var error in errors)
        {
            if (validationErrors.TryGetValue(error.Code, out var errorMessages))
            {
                errorMessages.Add(error.Message);
            }
            else
            {
                validationErrors.Add(error.Code, new List<string>
                {
                    error.Message
                });
            }
        }

        return TypedResults.ValidationProblem(
            errors: validationErrors.ToDictionary(k => k.Key, v => v.Value.ToArray()),
            detail: "Please refer to the errors property for additional details",
            instance: httpContext.Request.Path,
            title: "One or more validation errors occurred.",
            type: "https://httpwg.org/specs/rfc9110.html#status.400");
    }
}
