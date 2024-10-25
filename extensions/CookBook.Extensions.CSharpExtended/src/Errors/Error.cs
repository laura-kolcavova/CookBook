namespace CookBook.Extensions.CSharpExtended.Errors;

public record Error
{
    public string Code { get; }

    public string Message { get; }

    public ErrorType Type { get; }

    public ErrorSeverity Severity { get; }

    private Error(
        string code,
        string message,
        ErrorType type,
        ErrorSeverity severity)
    {
        Code = code;
        Message = message;
        Type = type;
        Severity = severity;
    }

    public static Error Failure(
        string code = GeneralErrorCodes.Failure,
        string message = "A failure error has occurred",
        ErrorSeverity severity = ErrorSeverity.Error)
    {
        return new Error(code, message, ErrorType.Failure, severity);
    }

    public static Error Validation(
        string code = GeneralErrorCodes.Validation,
        string message = "A validation error has occurred",
        ErrorSeverity severity = ErrorSeverity.Error)
    {
        return new Error(code, message, ErrorType.Validation, severity);
    }

    public static Error Unauthorized(
        string code = GeneralErrorCodes.Unauthorized,
        string message = "An 'Unauthorized' error has occurred",
        ErrorSeverity severity = ErrorSeverity.Error)
    {
        return new Error(code, message, ErrorType.Unauthorized, severity);
    }

    public static Error NotEnoughRights(
        string code = GeneralErrorCodes.NotEnoughRights,
        string message = "A 'Not enough rights' error has occurred",
        ErrorSeverity severity = ErrorSeverity.Error)
    {
        return new Error(code, message, ErrorType.NotEnoughRights, severity);
    }

    public static Error NotFound(
        string code = GeneralErrorCodes.NotFound,
        string message = "A 'Not found' error has occurred",
        ErrorSeverity severity = ErrorSeverity.Error)
    {
        return new Error(code, message, ErrorType.NotFound, severity);
    }

    public static Error Timeout(
        string code = GeneralErrorCodes.Timeout,
        string message = "A 'Timeout' error has occurred",
        ErrorSeverity severity = ErrorSeverity.Error)
    {
        return new Error(code, message, ErrorType.Timeout, severity);
    }

    public static Error Conflict(
        string code = GeneralErrorCodes.Conflict,
        string message = "A 'Conflict' error has occurred",
        ErrorSeverity severity = ErrorSeverity.Error)
    {
        return new Error(code, message, ErrorType.Conflict, severity);
    }

    public static Error OperationCancelled(
        string code = GeneralErrorCodes.OperationCancelled,
        string message = "An 'Operation Cancelled' error has occurred",
        ErrorSeverity severity = ErrorSeverity.Error)
    {
        return new Error(code, message, ErrorType.OperationCancelled, severity);
    }

    public static Error UnprocessableEntity(
        string code = GeneralErrorCodes.UnprocessableEntity,
        string message = "An 'Unprocessable Entity' error has occurred",
        ErrorSeverity severity = ErrorSeverity.Error)
    {
        return new Error(code, message, ErrorType.UnprocessableEntity, severity);
    }

    public static Error Unexpected(
        string code = GeneralErrorCodes.Unexpected,
        string message = "An unexpected error has occurred",
        ErrorSeverity severity = ErrorSeverity.Error)
    {
        return new Error(code, message, ErrorType.Unexpected, severity);
    }
}
