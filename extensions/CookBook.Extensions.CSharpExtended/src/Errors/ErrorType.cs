namespace CookBook.Extensions.CSharpExtended.Errors;

public enum ErrorType
{
    Failure = 0,

    Validation = 1,

    Unauthorized = 2,

    NotEnoughRights = 3,

    NotFound = 4,

    Timeout = 5,

    Conflict = 6,

    OperationCancelled = 7,

    UnprocessableEntity = 8,

    Unexpected = 9
}
