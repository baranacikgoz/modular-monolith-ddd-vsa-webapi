using System.Buffers;
using Common.Domain.ResultMonad;

namespace Common.Domain.Extensions;

public static class StringExtensions
{
    private static readonly SearchValues<char> _turkishAlphabet =
        SearchValues.Create("abcçdefgğhıijklmnoöprsştuüvyzABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ");

    private static readonly SearchValues<char> _turkishAlphabetWithEmptySpace =
        SearchValues.Create(" abcçdefgğhıijklmnoöprsştuüvyzABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ");

    public static Result EnsureNotNullOrEmpty(string? value, Error ifNullOrEmpty)
    {
        return string.IsNullOrEmpty(value)
            ? Result.Failure(ifNullOrEmpty)
            : Result.Success;
    }

    public static bool ContainsOnlyTurkishCharacters(this string s, bool allowWhiteSpace)
    {
        if (string.IsNullOrEmpty(s))
        {
            return false;
        }

        return allowWhiteSpace
            ? !s.AsSpan().ContainsAnyExcept(_turkishAlphabetWithEmptySpace)
            : !s.AsSpan().ContainsAnyExcept(_turkishAlphabet);
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
                    _ => char.ToUpperInvariant(c)
                };

                chars[i] = result;
            }
        });
    }
}
