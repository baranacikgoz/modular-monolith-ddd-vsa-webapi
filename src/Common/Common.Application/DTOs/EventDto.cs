using System.Text.Json;
using Common.Domain.Events;
using Common.Domain.StronglyTypedIds;

namespace Common.Application.DTOs;

public sealed record EventDto(JsonElement Event, DefaultIdType CreatedBy);
