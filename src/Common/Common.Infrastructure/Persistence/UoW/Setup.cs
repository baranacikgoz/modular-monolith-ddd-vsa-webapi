using Common.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Persistence.UoW;
public static class Setup
{
    public static IServiceCollection AddModuleUnitOfWork<TDbContext>(
        this IServiceCollection services,
        string moduleName)
        where TDbContext : DbContext
        => services.AddKeyedScoped<IUnitOfWork>(moduleName, (sp, _) =>
        {
            var context = sp.GetRequiredService<TDbContext>();
            return new UnitOfWork(context);
        });
}
