using Common.Application.Validation;
using FluentValidation;

namespace Common.Application.Options;

public class FullTextSearchOptions
{
    /// <summary>Fallback config name for unknown cultures (already includes the unaccent variant).</summary>
    public required string DefaultConfig { get; set; }

    /// <summary>When true, the resolver appends <c>_unaccent</c> to the base config of a culture.</summary>
    public required bool UseUnaccent { get; set; }

    /// <summary>Culture two-letter code → base Postgres text-search config (e.g. "tr" → "turkish").</summary>
    public Dictionary<string, string> CultureToConfig { get; init; } = [];

    /// <summary>
    /// ts_rank weight map in Postgres/Npgsql order {D, C, B, A}. Universal (name) layer is weight A,
    /// prose layer B/C, so a higher A weight ranks name hits above description hits.
    /// </summary>
    public IReadOnlyList<float> RankWeights { get; init; } = [];

    /// <summary>Language-neutral config used for the universal layer on both index and query side.</summary>
    public const string UniversalConfig = "simple_unaccent";

    /// <summary>Shadow/computed tsvector column name — single source of truth for index and query.</summary>
    public const string SearchVectorColumn = "SearchVector";

    /// <summary>Per-row authored-language column name (prose entities only).</summary>
    public const string LanguageColumn = "Language";

    /// <summary>Index method for the search vector.</summary>
    public const string IndexMethod = "GIN";
}

public class FullTextSearchOptionsValidator : CustomValidator<FullTextSearchOptions>
{
    public FullTextSearchOptionsValidator()
    {
        RuleFor(x => x.DefaultConfig)
            .NotEmpty()
            .WithMessage("DefaultConfig must not be empty.");

        RuleFor(x => x.CultureToConfig)
            .NotNull()
            .NotEmpty()
            .WithMessage("CultureToConfig must contain at least one culture mapping.");

        RuleFor(x => x.RankWeights)
            .NotNull()
            .Must(w => w.Count == 4)
            .WithMessage("RankWeights must contain exactly 4 values in {D, C, B, A} order.");
    }
}
