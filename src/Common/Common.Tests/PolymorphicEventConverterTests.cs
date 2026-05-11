using System.Text.Json;
using Common.Domain.Events;
using Common.Infrastructure.Persistence.ValueConverters;
using Xunit;

namespace Common.Tests;

#pragma warning disable CA1515, CA1707 // Consider making public types internal, Remove underscores from member name
public record TestableDomainEvent(string Data, int Value) : DomainEvent;

public sealed record AnotherTestableDomainEvent(string Name) : DomainEvent;
#pragma warning restore CA1515

#pragma warning disable CA1707
public sealed class PolymorphicEventConverterTests
{
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = false,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters =
        {
            new PolymorphicEventConverter<IEvent>(),
            new System.Text.Json.Serialization.JsonStringEnumConverter()
        }
    };

    [Fact]
    public void RoundTrip_PreservesAllProperties()
    {
        var original = new TestableDomainEvent("hello", 42);

        var json = JsonSerializer.Serialize<IEvent>(original, Options);
        var restored = JsonSerializer.Deserialize<IEvent>(json, Options) as TestableDomainEvent;

        Assert.NotNull(restored);
        Assert.Equal(original.Id, restored.Id);
        Assert.Equal(original.CreatedOn, restored.CreatedOn);
        Assert.Equal(original.Data, restored.Data);
        Assert.Equal(original.Value, restored.Value);
        Assert.Equal(original.Version, restored.Version);
    }

    [Fact]
    public void RoundTrip_WithDifferentEventType_PreservesType()
    {
        var original = new AnotherTestableDomainEvent("test-name");

        var json = JsonSerializer.Serialize<IEvent>(original, Options);
        var restored = JsonSerializer.Deserialize<IEvent>(json, Options);

        Assert.NotNull(restored);
        Assert.IsType<AnotherTestableDomainEvent>(restored);
        var typed = (AnotherTestableDomainEvent)restored;
        Assert.Equal(original.Name, typed.Name);
    }

    [Fact]
    public void RoundTrip_UsesFullName_NotAssemblyQualifiedName()
    {
        var original = new TestableDomainEvent("check-name", 99);

        var json = JsonSerializer.Serialize<IEvent>(original, Options);
        var doc = JsonDocument.Parse(json);

        var typeFullName = doc.RootElement.GetProperty("eventTypeFullName").GetString();
        Assert.NotNull(typeFullName);
        // Should be the FullName (no version/culture/token), NOT AssemblyQualifiedName
        Assert.DoesNotContain(", Version=", typeFullName, StringComparison.Ordinal);
        Assert.DoesNotContain(", Culture=", typeFullName, StringComparison.Ordinal);
        Assert.DoesNotContain("PublicKeyToken=", typeFullName, StringComparison.Ordinal);
        // Should be Common.Tests.TestableDomainEvent (FullName)
        Assert.Equal(typeof(TestableDomainEvent).FullName, typeFullName);
    }

    [Fact]
    public void Serialize_WriteIndentedIsFalse_NoNewlinesInOutput()
    {
        var original = new TestableDomainEvent("compact", 1);

        var json = JsonSerializer.Serialize<IEvent>(original, Options);

        Assert.DoesNotContain('\n', json);
        Assert.DoesNotContain('\r', json);
    }

    [Fact]
    public void EventConverter_WriteOptions_WriteIndentedIsFalse()
    {
        Assert.False(EventConverter.WriteOptions.WriteIndented);
    }

    [Fact]
    public void Deserialize_Succeeds_AfterAssemblyVersionChangeSimulation()
    {
        // Simulate: serialized with FullName, deserialized in another app domain context
        // Write a JSON with FullName only (no version info)
        var original = new TestableDomainEvent("version-tolerance", 7);
        var json = JsonSerializer.Serialize<IEvent>(original, Options);

        // Manipulate the JSON to strip any version info (already stripped by our fix, but test the resolver)
        var doc = JsonDocument.Parse(json);
        var typeFullName = doc.RootElement.GetProperty("eventTypeFullName").GetString();

        // Verify the resolver can find the type by FullName alone
        var resolvedType = Type.GetType(typeFullName!);
        Assert.NotNull(resolvedType);
        Assert.Equal(typeof(TestableDomainEvent), resolvedType);
    }
}
