using Common.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Products.Application.Exports;
using Products.Application.Imports;
using Products.Domain.Products;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;

namespace Products.Application.Persistence;

public interface IProductsDbContext : IDbContext
{
    public DbSet<Store> Stores { get; }
    public DbSet<ProductTemplate> ProductTemplates { get; }
    public DbSet<Product> Products { get; }
    public DbSet<ProductImportJob> ImportJobs { get; }
    public DbSet<ProductExportJob> ExportJobs { get; }
}
