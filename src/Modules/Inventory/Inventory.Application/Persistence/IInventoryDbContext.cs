using Common.Application.Persistence;
using Inventory.Domain.StockLevels;
using Inventory.Domain.StockReservations;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Application.Persistence;

public interface IInventoryDbContext : IDbContext
{
    public DbSet<StockLevel> StockLevels { get; }
    public DbSet<StockReservation> StockReservations { get; }
}
