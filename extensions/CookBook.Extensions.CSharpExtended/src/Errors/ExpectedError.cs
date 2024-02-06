namespace CookBook.Extensions.CSharpExtended.Errors;

public record ExpectedError
{
    public string Code { get; }

    public string Message { get; }

    public ExpectedErrorType Type { get; }

    public ExpectedErrorSeverity Severity { get; }

    private ExpectedError(
        string code,
        string message,
        ExpectedErrorType type,
        ExpectedErrorSeverity severity)
    {
        Code = code;
        Message = message;
        Type = type;
        Severity = severity;
    }

    public static ExpectedError Failure(
        string code = GeneralErrorCodes.Failure,
        string message = "A failure has occurred",
        ExpectedErrorSeverity severity = ExpectedErrorSeverity.Error)
    {
        return new ExpectedError(code, message, ExpectedErrorType.Failure, severity);
    }

    public static ExpectedError Validation(
        string code = GeneralErrorCodes.Validation,
        string message = "A validation error has occurred",
        ExpectedErrorSeverity severity = ExpectedErrorSeverity.Error)
    {
        return new ExpectedError(code, message, ExpectedErrorType.Validation, severity);
    }

    public static ExpectedError NotFound(
        string code = GeneralErrorCodes.NotFound,
        string message = "A 'Not found' error has occurred",
        ExpectedErrorSeverity severity = ExpectedErrorSeverity.Error)
    {
        return new ExpectedError(code, message, ExpectedErrorType.NotFound, severity);
    }

    public static ExpectedError Unauthorized(
        string code = GeneralErrorCodes.Unauthorized,
        string message = "An 'Unauthorized' error has occurred",
        ExpectedErrorSeverity severity = ExpectedErrorSeverity.Error)
    {
        return new ExpectedError(code, message, ExpectedErrorType.Unauthorized, severity);
    }

    public static ExpectedError NotEnoughRights(
        string code = GeneralErrorCodes.NotEnoughRights,
        string message = "A 'Not enough rights' error has occurred",
        ExpectedErrorSeverity severity = ExpectedErrorSeverity.Error)
    {
        return new ExpectedError(code, message, ExpectedErrorType.NotEnoughRights, severity);
    }
}
