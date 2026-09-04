using System.Reflection;
using System.Text.Json;
using Common.Application.JsonConverters;
using Common.Domain.StronglyTypedIds;
using Common.Tests.Architecture;
using Xunit;

namespace Common.Tests;

#pragma warning disable CA1515, CA1707 // Consider making public types internal, Remove underscores from member name

/// <summary>
/// Every <see cref="IStronglyTypedId"/> implementation must round-trip through the write/read
/// JSON converter pair used by <c>DomainEventConverter</c>/<c>IntegrationEventConverter</c>.
/// Guards <c>StronglyTypedIdReadOnlyJsonConverter</c>'s <c>Activator.CreateInstance(type, value)</c>
/// construction, which silently breaks for any id type that stops having a single
/// <see cref="DefaultIdType"/> constructor.
/// </summary>
public sealed class StronglyTypedIdRoundTripTests
{
    private static readonly JsonSerializerOptions _writeOptions = new()
    {
        Converters = { new StronglyTypedIdWriteOnlyJsonConverter() }
    };

    private static readonly JsonSerializerOptions _readOptions = new()
    {
        Converters = { new StronglyTypedIdReadOnlyJsonConverter() }
    };

    public static TheoryData<Type> StronglyTypedIdTypes()
    {
        var data = new TheoryData<Type>();

        var types = SolutionAssemblies.All
            .SelectMany(GetLoadableTypes)
            .Where(t => t is { IsAbstract: false, IsInterface: false }
                        && typeof(IStronglyTypedId).IsAssignableFrom(t));

        foreach (var type in types)
        {
            data.Add(type);
        }

        return data;
    }

    [Theory]
    [MemberData(nameof(StronglyTypedIdTypes))]
    public void RoundTrips_ThroughWriteThenReadConverter(Type idType)
    {
        var original = (IStronglyTypedId)Activator.CreateInstance(idType, DefaultIdType.CreateVersion7())!;

        var json = JsonSerializer.Serialize(original, idType, _writeOptions);
        var restored = (IStronglyTypedId)JsonSerializer.Deserialize(json, idType, _readOptions)!;

        Assert.IsType(idType, restored);
        Assert.Equal(original.Value, restored.Value);
    }

    private static IEnumerable<Type> GetLoadableTypes(Assembly assembly)
    {
        try
        {
            return assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException ex)
        {
            return ex.Types.Where(t => t is not null)!;
        }
    }
}

#pragma warning restore CA1515, CA1707
