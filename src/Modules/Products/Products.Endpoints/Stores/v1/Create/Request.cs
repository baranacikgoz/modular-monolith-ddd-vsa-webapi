using System.Text.Json.Serialization;
using Common.Application.JsonConverters;
using Common.Domain.StronglyTypedIds;

namespace Products.Endpoints.Stores.v1.Create;

public sealed record Request(string Name, string Description, string Address)
{
    [JsonConverter(typeof(StronglyTypedIdReadOnlyJsonConverter<ApplicationUserId>))]
    public ApplicationUserId OwnerId { get; init; }
}
