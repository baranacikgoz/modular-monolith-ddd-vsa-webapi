namespace Common.Application.Extensions;

public static class CollectionExtensions
{
    public static string JoinWithComma(this ICollection<string> collection)
    {
        return string.Join(",", collection);
    }
}
