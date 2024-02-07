using System.Buffers;
using Common.Core.Contracts.Results;

namespace Common.Core.Extensions;

public static class StringExt
{
    public static Result<string> EnsureNotNullOrEmpty(string? value, Error ifNullOrEmpty)
        => string.IsNullOrEmpty(value)
            ? Result<string>.Failure(ifNullOrEmpty)
            : Result<string>.Success(value!);

    private static readonly SearchValues<char> _turkishAlphabet = SearchValues.Create("abcçdefgğhıijklmnoöprsştuüvyzABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ");
    private static readonly SearchValues<char> _turkishAlphabetWithEmptySpace = SearchValues.Create(" abcçdefgğhıijklmnoöprsştuüvyzABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ");
    public static bool ContainsOnlyTurkishCharacters(this string s, bool allowWhiteSpace)
    {
        if (string.IsNullOrEmpty(s))
        {
            return false;
        }

        if (allowWhiteSpace)
        {
            return !s.AsSpan().ContainsAnyExcept(_turkishAlphabetWithEmptySpace);
        }

        return !s.AsSpan().ContainsAnyExcept(_turkishAlphabet);
    }
}
