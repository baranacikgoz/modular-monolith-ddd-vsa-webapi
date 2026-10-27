using System.Security.Cryptography;
using System.Text;

namespace Products.Infrastructure.Caching;

/// <summary>
///     FusionCache keys of the Products module, one method per cached call. Every input is hashed, so a long term still
///     yields a short, stable key. Module-local on purpose: <c>Common</c> carries no module's keys.
/// </summary>
public static class ProductsCacheKeys
{
    /// <param name="term">The trimmed search term.</param>
    /// <param name="proseConfig">The caller's resolved full-text config: the same term can match differently per language.</param>
    /// <param name="cap">FullTextSearchOptions.MaxMatchResults at the time of the call: it shapes the answer.</param>
    public static string ProductSearch(string term, string proseConfig, int cap)
    {
        return $"products:product-search:{Hash($"{term}\n{proseConfig}\n{cap}")}";
    }

    private static string Hash(string input)
    {
        return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(input)));
    }
}
