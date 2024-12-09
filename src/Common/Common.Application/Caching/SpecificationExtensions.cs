using Ardalis.Specification;

namespace Common.Application.Caching;

public static class SpecificationExtensions
{
    public static ICacheSpecificationBuilder<T> WithCacheTags<T>(
        this ICacheSpecificationBuilder<T> specificationBuilder,
        params string[] tags) where T : class
    {
        specificationBuilder.Specification.Items["CacheTags"] ??= new List<string>();
        ((List<string>)specificationBuilder.Specification.Items["CacheTags"]).AddRange(tags);

        return specificationBuilder;
    }

    public static string[] GetCacheTags<T>(this ISpecification<T> specification) where T : class
    {
        if (specification.Items.TryGetValue("CacheTags", out var tags))
        {
            return (string[])tags;
        }

        return [];
    }
}
