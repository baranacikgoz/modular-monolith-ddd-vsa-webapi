using Common.Domain.Events;
using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.Persistence.EventSourcing;
using IdentityAndAuth.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityAndAuth.Infrastructure.Persistence;

public class IdentityAndAuthDbContext(
    DbContextOptions<IdentityAndAuthDbContext> options
    ) : IdentityDbContext<ApplicationUser, IdentityRole<ApplicationUserId>, ApplicationUserId, IdentityUserClaim<ApplicationUserId>, IdentityUserRole<ApplicationUserId>, IdentityUserLogin<ApplicationUserId>, IdentityRoleClaim<ApplicationUserId>, IdentityUserToken<ApplicationUserId>>(options)
{
    public DbSet<EventStoreEvent> EventStoreEvents => Set<EventStoreEvent>();
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema(nameof(IdentityAndAuth));
        builder.ApplyConfigurationsFromAssembly(typeof(IdentityAndAuthDbContext).Assembly);

        builder.Ignore<DomainEvent>();
        builder.ApplyConfiguration(new EventStoreEventConfiguration());
    }

}
