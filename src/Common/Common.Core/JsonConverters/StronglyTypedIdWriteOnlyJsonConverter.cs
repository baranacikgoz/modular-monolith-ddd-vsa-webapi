using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Common.Core.Contracts;

namespace Common.Core.JsonConverters;
public class StronglyTypedIdWriteOnlyJsonConverter : JsonConverter<IStronglyTypedId>
{
    public override bool CanConvert(Type typeToConvert)
        => typeof(IStronglyTypedId).IsAssignableFrom(typeToConvert);

    public override IStronglyTypedId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => throw new NotImplementedException("Direct deserialization is not supported, use something like [ModelBinder<StronglyTypedIdBinder<ProductId>>]");

    public override void Write(Utf8JsonWriter writer, IStronglyTypedId value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.Value.ToString());
}
