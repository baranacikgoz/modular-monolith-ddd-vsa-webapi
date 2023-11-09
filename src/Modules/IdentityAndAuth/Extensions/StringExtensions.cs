namespace IdentityAndAuth.Extensions;

internal static class StringExtensions
{
    public static string TrimmedUpperInvariantTransliterateTurkishChars(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
        }

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
