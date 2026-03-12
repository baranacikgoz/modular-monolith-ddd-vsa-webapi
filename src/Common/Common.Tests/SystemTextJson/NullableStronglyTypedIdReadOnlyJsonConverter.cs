using System.Text.Json;
using System.Text.Json.Serialization;
using Common.Domain.StronglyTypedIds;

namespace Common.Tests.SystemTextJson;

public class NullableStronglyTypedIdReadOnlyJsonConverter : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        if (typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            var underlyingType = Nullable.GetUnderlyingType(typeToConvert);
            return underlyingType != null && typeof(IStronglyTypedId).IsAssignableFrom(underlyingType);
        }
        return false;
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var underlyingType = Nullable.GetUnderlyingType(typeToConvert)!;
        var converterType = typeof(NullableConverter<>).MakeGenericType(underlyingType);
        return (JsonConverter)Activator.CreateInstance(converterType)!;
    }

    private class NullableConverter<T> : JsonConverter<Nullable<T>> where T : struct, IStronglyTypedId
    {
        public override Nullable<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

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
            if (string.IsNullOrWhiteSpace(stringValue)) return null;
            
            var parsedValue = DefaultIdType.Parse(stringValue);
            return (T)Activator.CreateInstance(typeof(T), parsedValue)!;
        }

        public override void Write(Utf8JsonWriter writer, Nullable<T> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
