using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Products.Infrastructure.Persistence.EntityConfigurations;

/// <summary>
///     Stores a list of records that is always read and written whole as one jsonb column. The list is compared by its
///     serialized form: a record that holds a list compares that list by reference, and a list rebuilt with the same
///     items would otherwise mark the row modified on every load. Not for data a query filters or joins on.
/// </summary>
internal static class JsonbListExtensions
{
    private static readonly JsonSerializerOptions _options = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter() }
    };

    public static PropertyBuilder<IReadOnlyList<T>> HasJsonbList<T>(this PropertyBuilder<IReadOnlyList<T>> property)
    {
        return property
            .HasConversion(
                list => Serialize(list),
                json => Deserialize<T>(json),
                new ValueComparer<IReadOnlyList<T>>(
                    (a, b) => Serialize(a!) == Serialize(b!),
                    list => Serialize(list).GetHashCode(StringComparison.Ordinal),
                    list => Deserialize<T>(Serialize(list))))
            .HasColumnType("jsonb")
            .IsRequired();
    }

    private static string Serialize<T>(IReadOnlyList<T> list)
    {
        return JsonSerializer.Serialize(list, _options);
    }

    private static List<T> Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<List<T>>(json, _options) ?? [];
    }
}
