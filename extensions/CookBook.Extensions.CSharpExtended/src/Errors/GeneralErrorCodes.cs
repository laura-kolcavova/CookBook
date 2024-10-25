﻿namespace CookBook.Extensions.CSharpExtended.Errors;

public static class GeneralErrorCodes
{
    private const string prefix = "General";

    public const string Failure = $"{prefix}.{nameof(Failure)}";

    public const string Validation = $"{prefix}.{nameof(Validation)}";

    public const string Unauthorized = $"{prefix}.{nameof(Unauthorized)}";

    public const string NotEnoughRights = $"{prefix}.{nameof(NotEnoughRights)}";

    public const string NotFound = $"{prefix}.{nameof(NotFound)}";

    public const string Timeout = $"{prefix}.{nameof(Timeout)}";

    public const string Conflict = $"{prefix}.{nameof(Conflict)}";

    public const string OperationCancelled = $"{prefix}.{nameof(OperationCancelled)}";

    public const string UnprocessableEntity = $"{prefix}.{nameof(UnprocessableEntity)}";

    public const string Unexpected = $"{prefix}.{nameof(Unexpected)}";
}
