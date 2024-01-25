namespace CookBook.Extensions.CSharpExtended.Errors;

public record ValidationError(
    string Message,
    string Code,
    ValidationErrorSeverity Severity = ValidationErrorSeverity.Error)
{
}
