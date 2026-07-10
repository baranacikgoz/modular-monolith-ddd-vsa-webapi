using Common.Application.Auth;
using Common.Domain.Entities;
using Common.Domain.Events;
using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.EventBus;
using Common.Infrastructure.Persistence;
using Common.Infrastructure.Persistence.EntityConfigurations;
using IAM.Application.Persistence;
using IAM.Domain.Identity;
using IAM.Domain.Identity.Sessions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IAM.Infrastructure.Persistence;

#pragma warning disable S101 // Types should be named in PascalCase
public class IAMDbContext(
#pragma warning restore S101 // Types should be named in PascalCase
    DbContextOptions<IAMDbContext> options,
    TimeProvider timeProvider,
    ICurrentUser currentUser,
    ILogger<IAMDbContext> logger,
    EventDispatcher eventDispatcher,
    IntegrationEventOutbox integrationEventOutbox
) : IdentityDbContext<ApplicationUser, IdentityRole<ApplicationUserId>, ApplicationUserId,
    IdentityUserClaim<ApplicationUserId>, IdentityUserRole<ApplicationUserId>, IdentityUserLogin<ApplicationUserId>,
    IdentityRoleClaim<ApplicationUserId>, IdentityUserToken<ApplicationUserId>>(options), IIAMDbContext
{
    public DbSet<AuditLogEntry> AuditLog => Set<AuditLogEntry>();
    public DbSet<Session> Sessions => Set<Session>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await OutboxSaveHelper.SaveWithOutboxAsync(
            this, timeProvider, currentUser, logger,
            eventDispatcher, integrationEventOutbox,
            ct => base.SaveChangesAsync(ct),
            cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema(nameof(IAM));
        builder.ApplyConfigurationsFromAssembly(typeof(IAMDbContext).Assembly);

        builder.Ignore<DomainEvent>();
        builder.ApplyConfiguration(new AuditLogEntryConfiguration());
    }
}
