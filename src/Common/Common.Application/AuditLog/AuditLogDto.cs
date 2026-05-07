using System.Text.Json;
using Common.Domain.StronglyTypedIds;

namespace Common.Application.AuditLog;

public sealed record AuditLogDto(
    DateTimeOffset CreatedOn,
    string EventType,
    long Version,
    JsonElement Payload,
    ApplicationUserId CreatedBy);
