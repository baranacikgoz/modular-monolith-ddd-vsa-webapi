using System.Text.Json;

namespace Common.Application.EventHistory;

public sealed record EventDto(JsonElement Event, DefaultIdType CreatedBy);
