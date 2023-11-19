using Common.Core.Contracts.Results;

namespace Common.Core;

public static class StringExt
{
    public static Result<string> EnsureNotNullOrEmpty(string? value, Error ifNull)
        => string.IsNullOrEmpty(value)
            ? Result<string>.Failure(ifNull)
            : Result<string>.Success(value!);
}
