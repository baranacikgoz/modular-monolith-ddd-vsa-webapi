using System.Buffers;
using Common.Core.Contracts.Results;

namespace Common.Core.Extensions;

public static class StringExt
{
    public static Result EnsureNotNullOrEmpty(string? value, Error ifNullOrEmpty)
        => string.IsNullOrEmpty(value)
            ? Result.Failure(ifNullOrEmpty)
            : Result.Success;

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

    public static string TrimmedUpperInvariantTransliterateTurkishChars(this string value)
    {
        value = value.Trim();

        return string.Create(value.Length, value, (chars, state) =>
        {
            for (var i = 0; i < state.Length; i++)
            {
                var c = state[i];

                var result = c switch
                {
                    'ş' => 'S',
                    'Ş' => 'S',
                    'ğ' => 'G',
                    'Ğ' => 'G',
                    'ç' => 'C',
                    'Ç' => 'C',
                    'ö' => 'O',
                    'Ö' => 'O',
                    'ü' => 'U',
                    'Ü' => 'U',
                    'ı' => 'I',
                    'İ' => 'I',
                    _ => char.ToUpperInvariant(c),
                };

                chars[i] = result;
            }
        });
    }

}
