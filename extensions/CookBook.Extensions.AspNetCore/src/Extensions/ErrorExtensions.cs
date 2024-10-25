using CookBook.Extensions.CSharpExtended.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace CookBook.Extensions.AspNetCore.Extensions;
public static class ErrorExtensions
{
    public static ProblemDetails AsProblemDetails(
        this Error error, HttpContext httpContext)
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
            case ErrorType.Failure:
                statusCode = StatusCodes.Status400BadRequest;
                title = "A failure error has occurred";
                type = "https://httpwg.org/specs/rfc9110.html#status.400";
                break;
            case ErrorType.Validation:
                statusCode = StatusCodes.Status400BadRequest;
                title = "A validation error has occurred";
                type = "https://httpwg.org/specs/rfc9110.html#status.400";
                break;
            case ErrorType.Unauthorized:
                statusCode = StatusCodes.Status401Unauthorized;
                title = "An 'Unauthorized' error has occurred";
                type = "https://httpwg.org/specs/rfc9110.html#status.401";
                break;
            case ErrorType.NotEnoughRights:
                statusCode = StatusCodes.Status403Forbidden;
                title = "A 'Not enough rights' error has occurred";
                type = "https://httpwg.org/specs/rfc9110.html#status.403";
                break;
            case ErrorType.NotFound:
                statusCode = StatusCodes.Status404NotFound;
                title = "A 'Not found' error has occurred";
                type = "https://httpwg.org/specs/rfc9110.html#status.404";
                break;
            case ErrorType.Timeout:
                statusCode = StatusCodes.Status408RequestTimeout;
                title = "A 'Timeout' error has occurred";
                type = "https://httpwg.org/specs/rfc9110.html#status.408";
                break;
            case ErrorType.Conflict:
                statusCode = StatusCodes.Status409Conflict;
                title = "A 'Conflict' error has occurred";
                type = "https://httpwg.org/specs/rfc9110.html#status.409";
                break;
            case ErrorType.OperationCancelled:
                statusCode = StatusCodes.Status499ClientClosedRequest;
                title = "An 'OperationCancelled' error has occurred";
                type = "https://httpwg.org/specs/rfc9110.html#status.499";
                break;
            case ErrorType.UnprocessableEntity:
                statusCode = StatusCodes.Status422UnprocessableEntity;
                title = "An 'UnprocessableEntity' error has occurred";
                type = "https://httpwg.org/specs/rfc9110.html#status.422";
                break;
            case ErrorType.Unexpected:
                statusCode = StatusCodes.Status500InternalServerError;
                title = "An unexpected error has occurred";
                type = "https://httpwg.org/specs/rfc9110.html#status.500";
                break;
            default:
                throw new InvalidEnumArgumentException(error.Type.ToString());
        }

        return new ProblemDetails()
        {
            Detail = error.Message,
            Instance = httpContext.Request.Path,
            Status = statusCode,
            Title = title,
            Type = type,
            Extensions = extensions
        };
    }
}
