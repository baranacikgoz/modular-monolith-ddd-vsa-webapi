using System.Globalization;
using Common.Application.Options;
using Microsoft.Extensions.Options;

namespace Common.Application.Search;

/// <summary>
/// Resolves the Postgres text-search config for the current request from <see cref="CultureInfo.CurrentUICulture"/>
/// — the same Accept-Language mechanism that drives <c>IResxLocalizer</c>. Used on the write side (what to stamp
/// into the <c>Language</c> column) and the read side (the prose-layer query config). No query parameter.
/// </summary>
public interface ISearchLanguageResolver
{
    /// <summary>Per-request prose-layer config (e.g. "turkish_unaccent"), falling back to the default config.</summary>
    string ResolveConfig();

    /// <summary>Language-neutral universal-layer config, used for every row regardless of authored language.</summary>
    string UniversalConfig { get; }
}

public sealed class SearchLanguageResolver(IOptions<FullTextSearchOptions> options) : ISearchLanguageResolver
{
    private const string UnaccentSuffix = "_unaccent";
    private readonly FullTextSearchOptions _options = options.Value;

    public string UniversalConfig => FullTextSearchOptions.UniversalConfig;

    public string ResolveConfig()
    {
        var twoLetter = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

        var baseConfig = _options.CultureToConfig.TryGetValue(twoLetter, out var config)
            ? config
            : _options.DefaultConfig;

        return _options.UseUnaccent ? AppendUnaccent(baseConfig) : baseConfig;
    }

    private static string AppendUnaccent(string config)
        => config.EndsWith(UnaccentSuffix, StringComparison.Ordinal) ? config : config + UnaccentSuffix;
}
