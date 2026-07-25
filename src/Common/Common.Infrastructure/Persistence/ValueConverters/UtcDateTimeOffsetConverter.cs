using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Common.Infrastructure.Persistence.ValueConverters;

/// <summary>
/// Npgsql only accepts DateTimeOffset with Offset=0 for 'timestamp with time zone' params/columns.
/// Registered model-wide via ConfigureConventions so every DateTimeOffset property/parameter is
/// normalized at the EF boundary — callers never need to remember to call ToUniversalTime().
/// </summary>
public sealed class UtcDateTimeOffsetConverter() : ValueConverter<DateTimeOffset, DateTimeOffset>(
    v => v.ToUniversalTime(),
    v => v);
