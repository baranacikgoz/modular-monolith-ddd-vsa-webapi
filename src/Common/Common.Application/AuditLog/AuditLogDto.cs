using System.Text.Json;
using Common.Domain.StronglyTypedIds;

namespace Common.Application.AuditLog;

public sealed record AuditLogDto(JsonElement Event, ApplicationUserId CreatedBy);
