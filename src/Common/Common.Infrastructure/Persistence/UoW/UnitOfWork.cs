using Common.Application.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.Persistence.UoW;
public class UnitOfWork(DbContext dbContext) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => dbContext.SaveChangesAsync(cancellationToken);
}
