using Common.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Common.Persistence.TransactionalOutbox;
internal static class Setup
{
    public static IServiceCollection AddOutboxAndInterceptor(this IServiceCollection services)
        => services
            .AddScoped<InsertOutboxMessagesInterceptor>()
            .AddDbContext<OutboxDbContext>((sp, options) =>
            {
                var connectionString = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value.ConnectionString;
                options
                .UseNpgsql(
                    connectionString,
                    o => o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, nameof(TransactionalOutbox)));
            });
}
