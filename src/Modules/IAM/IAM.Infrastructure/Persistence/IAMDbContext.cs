using Common.Application.Options;
using Common.Domain.Entities;
using Common.Domain.Events;
using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.Persistence.EntityConfigurations;
using IAM.Application.Persistence;
using IAM.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IAM.Infrastructure.Persistence;

#pragma warning disable S101 // Types should be named in PascalCase
public partial class IAMDbContext(
#pragma warning restore S101 // Types should be named in PascalCase
    DbContextOptions<IAMDbContext> options,
    IOptions<ObservabilityOptions> observabilityOptionsProvider,
    ILogger<IAMDbContext> logger
) : IdentityDbContext<ApplicationUser, IdentityRole<ApplicationUserId>, ApplicationUserId,
    IdentityUserClaim<ApplicationUserId>, IdentityUserRole<ApplicationUserId>, IdentityUserLogin<ApplicationUserId>,
    IdentityRoleClaim<ApplicationUserId>, IdentityUserToken<ApplicationUserId>>(options), IIAMDbContext
{
    public DbSet<EventStoreEvent> EventStoreEvents => Set<EventStoreEvent>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema(nameof(IAM));
        builder.ApplyConfigurationsFromAssembly(typeof(IAMDbContext).Assembly);

        builder.Ignore<DomainEvent>();
        builder.ApplyConfiguration(new EventStoreEventConfiguration());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        if (observabilityOptionsProvider.Value.LogGeneratedSqlQueries)
        {
            optionsBuilder.LogTo(
                sql => LoggerMessages.LogSql(logger, sql), // Log the SQL query
                new[] { DbLoggerCategory.Database.Command.Name }, // Only log database commands
                LogLevel.Information // Set the log level
            );
        }
    }

    private static partial class LoggerMessages
    {
        [LoggerMessage(Level = LogLevel.Debug, Message = "{Sql}")]
        public static partial void LogSql(ILogger logger, string sql);
    }
}
