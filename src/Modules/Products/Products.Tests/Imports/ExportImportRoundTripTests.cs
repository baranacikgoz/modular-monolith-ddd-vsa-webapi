using System.Text;
using Common.Application.Auth;
using Common.Application.EventBus;
using Common.Application.Jobs;
using Common.Application.Storage;
using Common.Domain.StronglyTypedIds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Products.Application.Exports;
using Products.Application.Imports;
using Products.Application.Persistence;
using Products.Domain.Products;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;
using Products.Infrastructure.Exports;
using Products.Infrastructure.Imports;
using Products.Tests.Support;
using Xunit;

namespace Products.Tests.Imports;

#pragma warning disable CA1707 // Identifiers should not contain underscores

/// <summary>
///     An exported file is a valid import input: the export is a CSV and the import reads workbooks, so a user opens the CSV
///     in a spreadsheet and saves it as .xlsx (<see cref="Workbooks.FromCsv" /> does the same). The columns the import reads
///     come first in the export, in the import's order, and the columns after the SKU (id, store, creation time) are ignored.
/// </summary>
public class ExportImportRoundTripTests(IntegrationTestWebAppFactory factory) : ProductsIntegrationTest(factory)
{
    private async Task<(Store Store, ProductTemplate Template)> SeedSourceStoreAsync()
    {
        var store = Store.Create(DefaultUser, $"Source {Guid.NewGuid():N}", "Description", "Address");
        var template = ProductTemplate.Create("Brand", "Model", "Color");
        Db.Stores.Add(store);
        Db.ProductTemplates.Add(template);
        store.AddProduct(Product.Create(store.Id, template.Id, "Lamp, desk", "Says \"bright\", dimmable", 5, 49.9m, "LAMP-1"));
        store.AddProduct(Product.Create(store.Id, template.Id, "Chair", "Mesh back", 2, 120m));
        store.AddProduct(Product.Create(store.Id, template.Id, "Shelf", "", 1, 300m, "SHELF-1"));
        await Db.SaveChangesAsync();
        Db.ChangeTracker.Clear();
        return (store, template);
    }

    private async Task<Store> SeedEmptyTargetStoreAsync()
    {
        var store = Store.Create(new ApplicationUserId(Guid.NewGuid()), $"Target {Guid.NewGuid():N}", "Description", "Address");
        Db.Stores.Add(store);
        await Db.SaveChangesAsync();
        Db.ChangeTracker.Clear();
        return store;
    }

    private async Task<string> ExportCsvAsync(StoreId storeId)
    {
        var job = ProductExportJob.Queue(DefaultUser, DateTimeOffset.UtcNow.AddMinutes(1), storeId);
        Db.ExportJobs.Add(job);
        await Db.SaveChangesAsync();
        Db.ChangeTracker.Clear();

        using var scope = Factory.Services.CreateScope();
        var runner = new ProductExportJobRunner(
            scope.ServiceProvider.GetRequiredService<IProductsDbContext>(),
            PrivateStore,
            TestOptions.Products(),
            scope.ServiceProvider.GetRequiredService<IBackgroundUserContext>(),
            TimeProvider.System,
            NullLogger<ProductExportJobRunner>.Instance);
        await runner.RunAsync(job.Id, CancellationToken.None);

        var finished = await Db.ExportJobs.AsNoTracking().SingleAsync(j => j.Id == job.Id);
        Assert.Equal(JobStatus.Succeeded, finished.Status);
        var bytes = PrivateStore.Read(finished.ObjectKey!);
        return Encoding.UTF8.GetString(bytes, Encoding.UTF8.GetPreamble().Length, bytes.Length - Encoding.UTF8.GetPreamble().Length);
    }

    private async Task<ProductImportJob> ImportAsync(byte[] workbook, StoreId storeId, ProductTemplateId templateId)
    {
        var jobId = ProductImportJobId.New();
        var objectKey = $"products/imports/{jobId}.xlsx";
        using (var content = new MemoryStream(workbook))
        {
            await PrivateStore.UploadAsync(new UploadObjectRequest(objectKey, content, "application/octet-stream"), CancellationToken.None);
        }

        var job = ProductImportJob.Queue(
            jobId, DefaultUser, DateTimeOffset.UtcNow, storeId, templateId, Guid.NewGuid().ToString("N"), "products.xlsx", objectKey, string.Empty);
        Db.ImportJobs.Add(job);
        await Db.SaveChangesAsync();
        Db.ChangeTracker.Clear();

        using var scope = Factory.Services.CreateScope();
        var services = scope.ServiceProvider;
        var runner = new ProductImportJobRunner(
            services.GetRequiredService<IProductsDbContext>(),
            PrivateStore,
            services.GetRequiredService<IIntegrationEventOutbox>(),
            TestOptions.Products(),
            TestOptions.BulkImport(),
            services.GetRequiredService<IBackgroundUserContext>(),
            TimeProvider.System,
            NullLogger<ProductImportJobRunner>.Instance);
        await runner.RunAsync(jobId, CancellationToken.None);

        return await Db.ImportJobs.AsNoTracking().SingleAsync(j => j.Id == jobId);
    }

    private async Task<List<(string Name, string Description, int Quantity, decimal Price, string? Sku)>> ProductsOfAsync(StoreId storeId)
    {
        var products = await Db.Products.AsNoTracking().Where(p => p.StoreId == storeId).OrderBy(p => p.Name).ToListAsync();
        return products.Select(p => (p.Name, p.Description, p.Quantity, p.Price, p.Sku)).ToList();
    }

    private static string Rejections(ProductImportJob job)
    {
        return "Rejected rows: " + string.Join(", ", job.RowErrors.Select(e => $"{e.RowNumber}:{e.Reason}"));
    }

    [Fact]
    public async Task Import_ExportedFileSavedAsWorkbook_CreatesTheSameProductsInAnotherStore()
    {
        var (source, template) = await SeedSourceStoreAsync();
        var target = await SeedEmptyTargetStoreAsync();
        var csv = await ExportCsvAsync(source.Id);

        var job = await ImportAsync(Workbooks.FromCsv(csv), target.Id, template.Id);

        Assert.Equal(JobStatus.Succeeded, job.Status);
        Assert.Equal(3, job.ProductsCreated);
        Assert.True(job.RowsRejected == 0, Rejections(job));
        Assert.Equal(await ProductsOfAsync(source.Id), await ProductsOfAsync(target.Id));
    }

    // The export guards a cell a spreadsheet would run as a formula with a leading apostrophe; the import takes it off, so these
    // names come back exactly, and the ones that merely start with an apostrophe are never touched.
    [Fact]
    public async Task Import_ExportedNamesThatLookLikeFormulas_ComeBackExactly()
    {
        var source = Store.Create(DefaultUser, $"Source {Guid.NewGuid():N}", "Description", "Address");
        var template = ProductTemplate.Create("Brand", "Model", "Color");
        Db.Stores.Add(source);
        Db.ProductTemplates.Add(template);
        string[] names =
        [
            "-5 degree sock", "+1 free", "=SUM(A1:A9)", "@home", "'Tis the season", "'=odd", "''-5", "rock 'n' roll"
        ];
        for (var i = 0; i < names.Length; i++)
        {
            source.AddProduct(Product.Create(source.Id, template.Id, names[i], $"-{names[i]}", 1, 10m, $"SKU-{i}"));
        }

        await Db.SaveChangesAsync();
        Db.ChangeTracker.Clear();
        var target = await SeedEmptyTargetStoreAsync();
        var csv = await ExportCsvAsync(source.Id);

        var job = await ImportAsync(Workbooks.FromCsv(csv), target.Id, template.Id);

        Assert.Equal(JobStatus.Succeeded, job.Status);
        Assert.True(job.RowsRejected == 0, Rejections(job));
        Assert.Equal(names.Length, job.ProductsCreated);
        var imported = await ProductsOfAsync(target.Id);
        Assert.Equal(names.Order(StringComparer.Ordinal), imported.Select(p => p.Name).Order(StringComparer.Ordinal));
        Assert.All(imported, p => Assert.Equal($"-{p.Name}", p.Description));
    }

    [Fact]
    public async Task Import_ExportedFileOfTheSameStore_ChangesNothing()
    {
        var (source, template) = await SeedSourceStoreAsync();
        var csv = await ExportCsvAsync(source.Id);
        var before = await ProductsOfAsync(source.Id);

        var job = await ImportAsync(Workbooks.FromCsv(csv), source.Id, template.Id);

        Assert.Equal(JobStatus.Succeeded, job.Status);
        Assert.Equal(0, job.ProductsCreated);
        Assert.Equal(0, job.ProductsUpdated);
        Assert.True(job.RowsRejected == 0, Rejections(job));
        Assert.Equal(before, await ProductsOfAsync(source.Id));
    }
}

#pragma warning restore CA1707
