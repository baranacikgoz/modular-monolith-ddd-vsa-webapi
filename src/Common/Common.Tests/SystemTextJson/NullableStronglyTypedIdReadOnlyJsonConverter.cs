using System.Text.Json;
using System.Text.Json.Serialization;
using Common.Domain.StronglyTypedIds;

namespace Common.Tests.SystemTextJson;

/// <summary>
/// A <see cref="JsonConverterFactory"/> that supports deserializing both nullable and non-nullable
/// <see cref="IStronglyTypedId"/> types from JSON. This is needed in tests because the application's
/// <see cref="Common.Application.JsonConverters.StronglyTypedIdWriteOnlyJsonConverter"/> intentionally
/// throws on deserialization (it's write-only for API responses). The test HTTP client needs to be
/// able to deserialize response bodies that contain strongly-typed IDs.
/// </summary>
public class NullableStronglyTypedIdReadOnlyJsonConverter : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        // Handle non-nullable strongly-typed IDs
        if (typeof(IStronglyTypedId).IsAssignableFrom(typeToConvert) && typeToConvert.IsValueType)
        {
            return true;
        }

        // Handle nullable strongly-typed IDs (Nullable<TId>)
        if (typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            var underlyingType = Nullable.GetUnderlyingType(typeToConvert);
            return underlyingType != null && typeof(IStronglyTypedId).IsAssignableFrom(underlyingType);
        }

        return false;
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        if (typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            var underlyingType = Nullable.GetUnderlyingType(typeToConvert)!;
            var converterType = typeof(NullableConverter<>).MakeGenericType(underlyingType);
            return (JsonConverter)Activator.CreateInstance(converterType)!;
        }
        else
        {
            var converterType = typeof(NonNullableConverter<>).MakeGenericType(typeToConvert);
            return (JsonConverter)Activator.CreateInstance(converterType)!;
        }
    }

    private static T ParseStronglyTypedId<T>(ref Utf8JsonReader reader) where T : struct, IStronglyTypedId
    {
        string? stringValue = null;
        if (reader.TokenType == JsonTokenType.StartObject)
        {
            using var doc = JsonDocument.ParseValue(ref reader);
            if (doc.RootElement.TryGetProperty("Value", out var valProp) || doc.RootElement.TryGetProperty("value", out valProp))
            {
                stringValue = valProp.GetString();
            }
        }
        else
        {
            stringValue = reader.GetString();
        }

        if (string.IsNullOrWhiteSpace(stringValue))
        {
            throw new JsonException($"Cannot convert null/empty string to {typeof(T).Name}");
        }

        var parsedValue = DefaultIdType.Parse(stringValue);
        return (T)Activator.CreateInstance(typeof(T), parsedValue)!;
    }

    private class NonNullableConverter<T> : JsonConverter<T> where T : struct, IStronglyTypedId
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => ParseStronglyTypedId<T>(ref reader);

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.Value.ToString());
    }

    private class NullableConverter<T> : JsonConverter<Nullable<T>> where T : struct, IStronglyTypedId
    {
        public override Nullable<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            return ParseStronglyTypedId<T>(ref reader);
        }

        public override void Write(Utf8JsonWriter writer, Nullable<T> value, JsonSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
            }
            else
            {
                writer.WriteStringValue(value.Value.Value.ToString());
            }
        }
    }
}
