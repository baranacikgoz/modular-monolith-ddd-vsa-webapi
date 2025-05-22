using System.Text.Json;

namespace Common.Application.DTOs;

public sealed record EventDto(JsonElement Event, DefaultIdType CreatedBy);
