using Common.Core.Contracts.Results;

namespace Common.Core.Extensions;

public static class StringExt
{
    public static Result<string> EnsureNotNullOrEmpty(string? value, Error ifNullOrEmpty)
        => string.IsNullOrEmpty(value)
            ? Result<string>.Failure(ifNullOrEmpty)
            : Result<string>.Success(value!);
}
