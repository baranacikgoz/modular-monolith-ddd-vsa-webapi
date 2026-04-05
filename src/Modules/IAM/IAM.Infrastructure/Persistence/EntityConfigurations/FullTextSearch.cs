namespace IAM.Infrastructure.Persistence.EntityConfigurations;

internal static class FullTextSearch
{
    internal const string SearchVectorColumnName = "SearchVector";
    internal const string GinIndexMethod = "GIN";
    internal const string Language = "english";
}
