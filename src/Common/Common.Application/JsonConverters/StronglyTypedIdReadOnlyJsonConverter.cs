using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Common.Domain.StronglyTypedIds;

namespace Common.Application.JsonConverters;

public class StronglyTypedIdReadOnlyJsonConverter<T> : JsonConverter<T>
    where T : IStronglyTypedId, new()
{
    public override bool CanConvert(Type typeToConvert)
        => typeof(IStronglyTypedId).IsAssignableFrom(typeToConvert);

    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString() ?? string.Empty;
        var guid = DefaultIdType.Parse(value);
        return new T { Value = guid };
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        => throw new NotImplementedException(
            "Direct serialization is not supported, use [JsonConverter(typeof(StronglyTypedIdWriteOnlyJsonConverter))]");
}

public class StronglyTypedIdReadOnlyJsonConverter : JsonConverter<IStronglyTypedId>
{
    /// <summary>
    /// The converter will only handle types implementing IStronglyTypedId whose names end with "Id".
    /// </summary>
    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(IStronglyTypedId).IsAssignableFrom(typeToConvert);
    }

    /// <summary>
    /// Reads a JSON string and converts it into the appropriate strongly typed ID instance.
    /// </summary>
    /// <param name="reader">The JSON reader</param>
    /// <param name="typeToConvert">The target type (a strongly typed ID)</param>
    /// <param name="options">JSON serializer options</param>
    /// <returns>An instance of IStronglyTypedId with the parsed value set</returns>
    public override IStronglyTypedId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Read JSON as string
        string? stringValue = reader.GetString();
        if (string.IsNullOrWhiteSpace(stringValue))
        {
            throw new JsonException("The ID value is null or empty.");
        }

        // Parse the string value to the underlying ID type (e.g., Guid)
        object parsedValue;
        try
        {
            parsedValue = DefaultIdType.Parse(stringValue);
        }
        catch (Exception ex)
        {
            throw new JsonException($"Error parsing the value '{stringValue}' for type {typeToConvert}.", ex);
        }

        // If the type is nullable, obtain its underlying type.
        Type effectiveType = Nullable.GetUnderlyingType(typeToConvert) ?? typeToConvert;

        // Create an instance of the effective (non-nullable) type.
        object instance = Activator.CreateInstance(effectiveType)
                          ?? throw new JsonException($"Could not create an instance of {effectiveType}.");

        // Ensure the instance implements IStronglyTypedId.
        if (instance is not IStronglyTypedId stronglyTypedId)
        {
            throw new JsonException($"Instance of {effectiveType} does not implement IStronglyTypedId.");
        }

        // Use reflection to set the "Value" property on the instance.
        var propertyInfo = effectiveType.GetProperty("Value", BindingFlags.Public | BindingFlags.Instance);
        if (propertyInfo is null || !propertyInfo.CanWrite)
        {
            throw new JsonException($"Property 'Value' not found or not writable on type {effectiveType}.");
        }

        try
        {
            propertyInfo.SetValue(stronglyTypedId, parsedValue);
        }
        catch (Exception ex)
        {
            throw new JsonException($"Error setting the 'Value' property on {effectiveType}.", ex);
        }

        return stronglyTypedId;
    }

    /// <summary>
    /// Writing is not supported in this converter.
    /// </summary>
    public override void Write(Utf8JsonWriter writer, IStronglyTypedId value, JsonSerializerOptions options)
    {
        throw new NotImplementedException(
            "Direct serialization is not supported; please use the write-only converter.");
    }
}
