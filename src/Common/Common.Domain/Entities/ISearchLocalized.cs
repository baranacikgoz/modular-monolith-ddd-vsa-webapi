namespace Common.Domain.Entities;

/// <summary>
/// Marks an aggregate whose search vector has a per-row authored-language (prose) layer. The
/// <c>Language</c> column holds the Postgres text-search config name and is stamped by
/// <c>ApplySearchLanguageInterceptor</c> on insert — the domain never reads ambient culture.
/// </summary>
public interface ISearchLocalized
{
    string Language { get; }
}
